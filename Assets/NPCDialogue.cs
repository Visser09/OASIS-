using UnityEngine;
using UnityEngine.Networking;
using System.Collections;

public class NPCDialogue : MonoBehaviour
{
    // Replace with your actual GPT backend API endpoint.
    public string apiUrl = "http://localhost:5000/gpt";  

    // Reference to a prefab for a city building or a city block.
    public GameObject cityBuildingPrefab;  

    // Call this method to start a dialogue with the NPC.
    public void StartDialogue(string prompt)
    {
        StartCoroutine(SendPrompt(prompt));
    }

    IEnumerator SendPrompt(string prompt)
    {
        WWWForm form = new WWWForm();
        form.AddField("prompt", prompt);

        UnityWebRequest www = UnityWebRequest.Post(apiUrl, form);
        yield return www.SendWebRequest();

        if (www.result != UnityWebRequest.Result.Success)
        {
            Debug.LogError("Error: " + www.error);
        }
        else
        {
            string responseText = www.downloadHandler.text;
            ProcessResponse(responseText);
        }
    }

    // Process the GPT response, looking for commands or keywords.
    void ProcessResponse(string response)
    {
        Debug.Log("GPT Response: " + response);
        
        // If the response includes the command for building the city, trigger the build.
        if (response.ToLower().Contains("build the city"))
        {
            BuildCity();
        }
        else
        {
            // Otherwise, just log the response for now.
            Debug.Log("Dialogue: " + response);
        }
    }

    // This method handles the city-building logic.
    void BuildCity()
    {
        Debug.Log("Building the city!");
        // Example: Instantiate one building at a fixed position.
        // You can expand this to spawn more objects or even a grid of buildings.
        Instantiate(cityBuildingPrefab, new Vector3(0, 0, 0), Quaternion.identity);
    }
}
