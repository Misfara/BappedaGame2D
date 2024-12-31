using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class QuestSelection : MonoBehaviour
{
    [SerializeField] private GameObject panelQuestName; // The panel containing the quests
    [SerializeField] private Image kirimBalonKeTetangga;         // Image to highlight for the quest letter
    [SerializeField] private Image kirimMinumankePakZulfan;
    [SerializeField] private Image beriBerkasKeAsep;
    [SerializeField] private Image beriTahuTemanKerja;            // Image to highlight for picking gold

    private bool kirimBalonKeTetanggaSelected = false;         // Tracks if the quest letter is selected
    private bool kirimMinumankePakZulfanSelected = false; 
    private bool beriBerkasKeAsepSelected = false;   
    private bool beriTahuTemanKerjaSelected = false;       // Tracks if picking gold is selected

    // Called when a quest letter is clicked
    public void OnKirimBalonKeTetangga()
    {
        if (panelQuestName != null)
        {
            kirimBalonKeTetanggaSelected = true;
            kirimMinumankePakZulfanSelected = false;
            beriBerkasKeAsepSelected =false;
            beriTahuTemanKerjaSelected =false;

            // Highlight the quest letter
            if (kirimBalonKeTetangga != null)
            {
                kirimBalonKeTetangga.color = new Color(1f, 0.9652871f, 0.514151f); // Yellow
            }

            // Reset picking gold color
            if (kirimMinumankePakZulfan != null)
            {
               kirimMinumankePakZulfan.color = new Color(1f, 1f, 1f, 0.3921569f); // Default
            }

             if (beriBerkasKeAsep != null)
            {
               beriBerkasKeAsep.color = new Color(1f, 1f, 1f, 0.3921569f); // Default
            }
             if (beriTahuTemanKerja != null)
            {
               beriTahuTemanKerja.color = new Color(1f, 1f, 1f, 0.3921569f); // Default
            }
        }
    }

    // Called when picking gold is clicked
    public void OnKirimMinumankePakZulfan()
    {
        if (panelQuestName != null)
        {
            
            kirimBalonKeTetanggaSelected = false;
            kirimMinumankePakZulfanSelected = true;
            beriBerkasKeAsepSelected =false;
            beriTahuTemanKerjaSelected =false;

              // Reset quest letter color
            if (kirimBalonKeTetangga != null)
            {
                kirimBalonKeTetangga.color = new Color(1f, 1f, 1f, 0.3921569f); // Default
            }

            // Highlight picking gold
            if (kirimMinumankePakZulfan != null)
            {
                kirimMinumankePakZulfan.color = new Color(1f, 0.9652871f, 0.514151f); // Yellow
            }
            if (beriBerkasKeAsep != null)
            {
               beriBerkasKeAsep.color = new Color(1f, 1f, 1f, 0.3921569f); // Default
            }
             if (beriTahuTemanKerja != null)
            {
               beriTahuTemanKerja.color = new Color(1f, 1f, 1f, 0.3921569f); // Default
            }
        }
    }

     public void OnBeriBerkasKeAsep()
    {
        if (panelQuestName != null)
        {
            
            kirimBalonKeTetanggaSelected = false;
            kirimMinumankePakZulfanSelected = false;
            beriBerkasKeAsepSelected =true;
            beriTahuTemanKerjaSelected =false;

              // Reset quest letter color
            if (kirimBalonKeTetangga != null)
            {
                kirimBalonKeTetangga.color = new Color(1f, 1f, 1f, 0.3921569f); // Default
            }

            // Highlight picking gold
            if (kirimMinumankePakZulfan != null)
            {
                kirimMinumankePakZulfan.color = new Color(1f, 1f, 1f, 0.3921569f); // Yellow
            }
            if (beriBerkasKeAsep != null)
            {
               beriBerkasKeAsep.color = new Color(1f, 0.9652871f, 0.514151f); // Default
            }
             if (beriTahuTemanKerja != null)
            {
               beriTahuTemanKerja.color = new Color(1f, 1f, 1f, 0.3921569f); // Default
            }
        }
    }

       public void OnBeriTahuTemanKerja()
    {
        if (panelQuestName != null)
        {
            
            kirimBalonKeTetanggaSelected = false;
            kirimMinumankePakZulfanSelected = false;
            beriBerkasKeAsepSelected =false;
            beriTahuTemanKerjaSelected =true;

              // Reset quest letter color
            if (kirimBalonKeTetangga != null)
            {
                kirimBalonKeTetangga.color = new Color(1f, 1f, 1f, 0.3921569f); // Default
            }

            // Highlight picking gold
            if (kirimMinumankePakZulfan != null)
            {
                kirimMinumankePakZulfan.color = new Color(1f, 1f, 1f, 0.3921569f); // Yellow
            }
            if (beriBerkasKeAsep != null)
            {
               beriBerkasKeAsep.color = new Color(1f, 1f, 1f, 0.3921569f); // Default
            }
             if (beriTahuTemanKerja != null)
            {
               beriTahuTemanKerja.color = new Color(1f, 0.9652871f, 0.514151f); // Default
            }
        }
    }
}
