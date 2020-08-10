using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AtommInventory : MonoBehaviour
{
    public KeyCode action, interaction;
    public AudioClip open, close, itemPickUp, documentPickUp;

    List<Slot> inventory;
    List<Document> docs;
    Transform documents, items, docView, canvas, containerItems, containerDocs, canvasContainer;
    GameObject inv, itemSlot, documentSlot, docViewer, container, erit, erdoc, fx;
    PlayerMove pm;
    PlayerLook pl;
    CanvasScaler cs;

    private void Start()
    {
        inventory = new List<Slot>();
        docs = new List<Document>();
        canvas = GameObject.Find("Canvas").transform;
        inv = Instantiate(Resources.Load<GameObject>("Core/AIS/AtommInventory"), canvas);
        documents = inv.transform.Find("Docs");
        items = inv.transform.Find("Items");
        itemSlot = Resources.Load<GameObject>("Core/AIS/AtommSlot");
        documentSlot = Resources.Load<GameObject>("Core/AIS/AtommDocument");
        docViewer = Resources.Load<GameObject>("Core/AIS/AtommViewer");
        container = Resources.Load<GameObject>("Core/AIS/AtommContainer");
        erit = Resources.Load<GameObject>("Core/AIS/ERIT");
        erdoc = Resources.Load<GameObject>("Core/AIS/ERDOC");
        fx = Resources.Load<GameObject>("Core/AIS/_FX");
        inv.SetActive(false);
        cs = canvas.GetComponent<CanvasScaler>();
        cs.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        cs.referenceResolution = new Vector2(1280, 720);
        cs.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;
        cs.matchWidthOrHeight = 0.5f;

        // CHECKS IF IS TEST PLAYER
        if (GetComponent<PlayerMove>() != null)
            pm = GetComponent<PlayerMove>();
        foreach (Transform child in transform)
            if (child.GetComponent<PlayerLook>() != null)
                pl = child.GetComponent<PlayerLook>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(action))
            ActionInventory();

        if (Input.GetKeyDown(interaction) && !inv.activeSelf)
            CheckRaycast();
    }

    private void CheckRaycast()
    {
        Transform playerCamera;
        gameObject.layer = LayerMask.NameToLayer(LayerMask.LayerToName(2));
        foreach (Transform child in transform)
        {
            if (child.gameObject.GetComponent<Camera>() != null)
            {
                playerCamera = child;
                RaycastHit hit;
                if (Physics.Raycast(playerCamera.position, playerCamera.forward, out hit, 2.5f))
                {
                    if (hit.collider.GetComponent<AtommItem>() != null)
                        GatherItem(hit.collider.GetComponent<AtommItem>());
                    else if (hit.collider.GetComponent<AtommDocument>() != null)
                        GatherDoc(hit.collider.GetComponent<AtommDocument>());
                    else if (hit.collider.GetComponent<AtommContainer>() != null)
                        ContainerActive(hit.collider.GetComponent<AtommContainer>());
                }
                gameObject.layer = LayerMask.NameToLayer(LayerMask.LayerToName(0));
                Refresh();
                return;
            }
        }
    }

    public void ContainerActive (AtommContainer atommC)
    {
        ActionInventory();

        GameObject cont = Instantiate(container, canvas);
        canvasContainer = cont.transform;
        containerItems = cont.transform.Find("_I");
        containerDocs = cont.transform.Find("_D");

        foreach(Slot slot in atommC.slots)
        {
            GameObject button = Instantiate(itemSlot, containerItems);
            if (slot.quantity != 1)
                button.transform.Find("Text").GetComponent<Text>().text = slot.quantity.ToString("");
            else
                button.transform.Find("Text").GetComponent<Text>().text = "";
            button.transform.Find("Image").GetComponent<Image>().sprite = slot.icon;
            button.GetComponent<Button>().onClick.AddListener(delegate { GatherItem(slot, atommC); });
        }

        foreach (Document document in atommC.documents)
        {
            GameObject button = Instantiate(documentSlot, containerDocs);
            button.transform.Find("Text").GetComponent<Text>().text = document.documentName;
            button.GetComponent<Button>().onClick.AddListener(delegate { GatherDoc(document, atommC); });
        }
    }

    public void ActionInventory()
    {
        if (docView != null)
            Destroy(docView.gameObject);

        if (canvasContainer != null)
            Destroy(canvasContainer.gameObject);

        inv.SetActive(!inv.activeSelf);
        if (pm != null && pl != null)
        { pm.enabled = !pm.enabled; pl.enabled = !pl.enabled; }
        Refresh();

        if (!inv.activeSelf)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            SpawnFX(close);
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            SpawnFX(open);
        }
    }

    public void Freeze(bool o)
    {
        if (pm != null && pl != null)
        { pm.enabled = !o; pl.enabled = !o; }
        Refresh();

        if (!o)
        {
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            SpawnFX(close);
        }
        else
        {
            Cursor.visible = true;
            Cursor.lockState = CursorLockMode.None;
            SpawnFX(open);
        }
    }

    private void GatherItem(AtommItem item)
    {
        if (inventory.Count == 24)
            return;
        inventory.Add(new Slot(item));
        Destroy(item.gameObject);
        SpawnFX(itemPickUp);
    }

    private void GatherDoc(AtommDocument document)
    {
        if (docs.Count == 5)
            return;
        docs.Add(new Document(document));
        Destroy(document.gameObject);
        SpawnFX(documentPickUp);
    }

    private void GatherItem(Slot item, AtommContainer atommC)
    {
        if (inventory.Count == 24)
            return;
        inventory.Add(item);
        atommC.slots.Remove(item);
        SpawnFX(itemPickUp);
        Refresh();
        RefreshContainer(atommC);
    }

    private void GatherDoc(Document document, AtommContainer atommC)
    {
        if (docs.Count == 5)
            return;
        docs.Add(document);
        atommC.documents.Remove(document);
        SpawnFX(documentPickUp);
        Refresh();
        RefreshContainer(atommC);
    }

    void RefreshContainer (AtommContainer atommC)
    {
        foreach (Transform child in containerItems)
            Destroy(child.gameObject);

        foreach (Transform child in containerDocs)
            Destroy(child.gameObject);

        foreach (Slot slot in atommC.slots)
        {
            GameObject button = Instantiate(itemSlot, containerItems);
            if (slot.quantity != 1)
                button.transform.Find("Text").GetComponent<Text>().text = slot.quantity.ToString("");
            else
                button.transform.Find("Text").GetComponent<Text>().text = "";
            button.transform.Find("Image").GetComponent<Image>().sprite = slot.icon;
            button.GetComponent<Button>().onClick.AddListener(delegate { GatherItem(slot, atommC); });
        }

        foreach (Document document in atommC.documents)
        {
            GameObject button = Instantiate(documentSlot, containerDocs);
            button.transform.Find("Text").GetComponent<Text>().text = document.documentName;
            button.GetComponent<Button>().onClick.AddListener(delegate { GatherDoc(document, atommC); });
        }
    }

    private void Refresh()
    {
        foreach (Transform child in items)
            Destroy(child.gameObject);

        foreach (Transform child in documents)
            Destroy(child.gameObject);

        foreach (Slot slot in inventory)
        {
            GameObject button = Instantiate(itemSlot, items);
            if (slot.quantity != 1)
                button.transform.Find("Text").GetComponent<Text>().text = slot.quantity.ToString("");
            else
                button.transform.Find("Text").GetComponent<Text>().text = "";
            button.transform.Find("Image").GetComponent<Image>().sprite = slot.icon;
            button.GetComponent<Button>().onClick.AddListener(delegate { Action(slot); });
        }

        foreach (Document document in docs)
        {
            GameObject button = Instantiate(documentSlot, documents);
            button.transform.Find("Text").GetComponent<Text>().text = document.documentName;
            button.GetComponent<Button>().onClick.AddListener(delegate { Action(document); });
        }
    }

    private void Action(Slot slot)
    {
        if (Resources.Load<GameObject>("Prefabs/" + slot.itemName) == null)
        {
            GameObject newItem = Instantiate(erit, transform.position, transform.rotation);
            newItem.GetComponent<AtommItem>().itemName = slot.itemName;
            newItem.GetComponent<AtommItem>().quantity = slot.quantity;
        }
        else
        {
            GameObject newItem = Instantiate(Resources.Load<GameObject>("Prefabs/" + slot.itemName), transform.position, transform.rotation);
            newItem.GetComponent<AtommItem>().itemName = slot.itemName;
            newItem.GetComponent<AtommItem>().quantity = slot.quantity;
        }
        inventory.Remove(slot);
        Refresh();
    }

    private void Action(Document document)
    {
        if (Input.GetKey(KeyCode.LeftControl))
        {
            if (Resources.Load<GameObject>("Prefabs/" + document.documentName) == null)
            {
                GameObject newDoc = Instantiate(erdoc, transform.position, transform.rotation);
                newDoc.GetComponent<AtommDocument>().documentName = document.documentName;
                newDoc.GetComponent<AtommDocument>().documentText = document.documentText;
            }
            else
            {
                GameObject newDoc = Instantiate(Resources.Load<GameObject>("Prefabs/" + document.documentName), transform.position, transform.rotation);
                newDoc.GetComponent<AtommDocument>().documentName = document.documentName;
                newDoc.GetComponent<AtommDocument>().documentText = document.documentText;
            }
            docs.Remove(document);
        }
        else
        {
            ActionInventory();
            docView = Instantiate(docViewer, canvas).transform;
            docView.Find("_text").GetComponent<Text>().text = document.documentText;
        }

        Refresh();
    }

    void SpawnFX (AudioClip clip)
    {
        if (clip == null)
            return;
        GameObject newFX = Instantiate(fx, this.transform);
        newFX.GetComponent<AudioSource>().clip = clip;
        newFX.GetComponent<AudioSource>().Play();
        Destroy(newFX, clip.length);
    }

    public bool LookFor(string item)
    {
        foreach (Slot slot in inventory)
            if (slot.itemName == item)
                return true;

        return false;
    }

    public bool LookFor(string item, int quantity)
    {
        foreach (Slot slot in inventory)
            if (slot.itemName == item)
                return true;

        return false;
    }

    public bool LookForAndRemove(string item)
    {
        foreach (Slot slot in inventory)
            if (slot.itemName == item)
            { inventory.Remove(slot); return true; }

        return false;
    }

    public bool LookForAndRemove(string item, int quantity)
    {
        foreach (Slot slot in inventory)
        {
            if (slot.itemName == item && slot.quantity >= quantity)
            {
                slot.quantity -= quantity;
                if (slot.quantity <= 0)
                    inventory.Remove(slot);
                return true;
            }
        }
        return false;
    }

    [System.Serializable]
    public class Slot
    {
        public string itemName;
        public int quantity;
        public Sprite icon;

        public Slot(AtommItem item)
        {
            this.itemName = item.itemName;
            this.quantity = item.quantity;
            this.icon = item.icon;
        }
    }

    [System.Serializable]
    public class Document
    {
        public string documentName;
        [TextArea(5, 10)]
        public string documentText;

        public Document(AtommDocument doc)
        {
            this.documentName = doc.documentName;
            this.documentText = doc.documentText;
        }
    }
}
