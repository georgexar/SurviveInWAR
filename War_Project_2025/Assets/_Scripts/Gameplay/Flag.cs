using UnityEngine;

public class Flag : MonoBehaviour
{
    [SerializeField] private GameObject collectMessageUI; // Assign στο inspector, πχ TextMeshProUGUI object
    private bool playerInRange = false;
    private WaveManager waveManager;

    private void Start()
    {
        GameObject wmObj = GameObject.Find("WaveManager"); // Βάλε ακριβώς το όνομα του αντικειμένου στη σκηνή
        if (wmObj != null)
            waveManager = wmObj.GetComponent<WaveManager>();

        if (collectMessageUI != null)
            collectMessageUI.SetActive(false); // Κρύψε το μήνυμα στην αρχή
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = true;
            if (collectMessageUI != null)
                collectMessageUI.SetActive(true); // Εμφάνιση μήνυμα
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInRange = false;
            if (collectMessageUI != null)
                collectMessageUI.SetActive(false); // Κρύψε μήνυμα όταν φεύγει
        }
    }

    private void Update()
    {
        if (playerInRange && InputManager.Instance.CollectAction.WasPressedThisFrame()) // Αντικατάστησε με το collect button
        {
            Collect();
        }
    }

    private void Collect()
    {
        waveManager.FlagCollected(); // Ενημέρωσε τον WaveManager
        if (collectMessageUI != null)
            collectMessageUI.SetActive(false); // Κρύψε το μήνυμα
        Destroy(gameObject); // Κατέστρεψε τη σημαία
    }
}
