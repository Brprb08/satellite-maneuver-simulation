using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

/**
* UIManager class manages the user interface for switching between free camera and tracking camera modes.
* It controls the visibility of UI panels and highlights the active button.
**/
public class UIManager : MonoBehaviour
{
    public static UIManager Instance { get; private set; }

    [Header("Buttons")]
    public Button freeCamButton;
    public Button trackCamButton;
    public Button feedbackButton;

    [Header("Panels")]
    public GameObject objectPlacementPanel;
    public GameObject objectInfoPanel;
    public GameObject thrustButtons;
    public GameObject apogeePerigeePanel;
    public GameObject feedbackPanel;
    public GameObject toggleOptionsPanel;
    public GameObject dropdown;

    [Header("UI")]
    public TMP_InputField nameInputField;
    public TMP_InputField massInputField;
    public TMP_InputField radiusInputField;
    public Button placeObjectButton;
    public TMP_InputField velocityInputField;
    public TMP_Text earthCamButtonText;
    public TextMeshProUGUI instructionText;
    public TextMeshProUGUI apogeeText;
    public TextMeshProUGUI perigeeText;
    public TextMeshProUGUI semiMajorAxisText;
    public TextMeshProUGUI eccentricityText;
    public TextMeshProUGUI orbitalPeriodText;
    public TextMeshProUGUI inclinationText;
    public TextMeshProUGUI raanText;

    [Header("UI Flags")]
    public bool showInstructionText = false;
    private bool isTracking = true;
    public bool earthCamPressed = true;

    /**
    * Setup the singleton for accessing UIManager
    **/
    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
    }

    /**
    * Initializes the UI elements and sets the default button states.
    **/
    private void Start()
    {
        instructionText.text =
    "<b>Welcome to the Orbit Simulator!</b>\n" +
    "<b>Track Cam Mode Activated!</b>\n\n" +
    "<b>CONTROLS:</b>\n" +
    "- Dropdown Menu: Select the tracked object.\n" +
    "- Right Mouse Button: Rotate the camera.\n" +
    "- Mousewheel: Zoom in/out.\n" +
    "- Time Scaler: Adjust time speed (Reset: 'R').\n" +
    "- Earth Cam Button: Toggle 'Earth Cam' or 'Satellite Cam'.\n" +
    "     * Earth Cam: Centers the view on Earth.\n" +
    "     * Satellite Cam: Centers the view on the selected satellite.\n\n" +
    "<b>MONITOR:</b> Altitude, Velocity, Apogee, Perigee\n\n" +
    "<b>THRUST:</b>\n" +
    "- Prograde / Retrograde: Speed up or slow down in orbit.\n" +
    "- Left / Right: Adjust lateral movement (changes inclination).\n" +
    "- Radial In / Radial Out: Thrust toward or away from the planet you're orbiting.\n\n" +
    "Monitor these values to observe orbital behaviors.\n" +
    "Switch to Free Cam to explore or place satellites.";



        ShowObjectPlacementPanel(false);
        ShowPanel(true);
        SetButtonState(freeCamButton, false);
        SetButtonState(trackCamButton, true);
        trackCamButton.Select();
        trackCamButton.interactable = false;

        feedbackPanel.SetActive(showInstructionText);
        UpdateButtonText();
    }

    /**
    * Handles the Free Cam button press event.
    **/
    public void OnFreeCamPressed()
    {
        instructionText.text =
        "<b>Free Cam Mode Activated!</b>\n\n" +
        "You can freely move to explore or place satellites.\n\n" +
        "<b>CONTROLS:</b>\n" +
        "- WASD: Move around.\n" +
        "- Right Mouse Button: Rotate the camera.\n\n" +
        "<b>PLACING A SATELLITE:</b>\n" +
        "- Naming is optional (defaults to 'Satellite (n)').\n" +
        "- Set Mass (500 - 5.0e23 kg).\n" +
        "- Set Radius (1-50).\n" +
        "  * Format: 5,45,3\n" +
        "  * No parentheses, negatives, or non-numeric characters.\n" +
        "- Click 'Place Satellite' to spawn.";

        ShowObjectPlacementPanel(true);
        ShowPanel(false);
        SetButtonState(freeCamButton, true);
        SetButtonState(trackCamButton, false);
        ShowThrustButtonsPanel(false);
        ShowApogeePerigeePanel(false);

        toggleOptionsPanel.SetActive(false);
        dropdown.SetActive(false);

        freeCamButton.interactable = false;
        trackCamButton.interactable = true;

        isTracking = false;

        if (velocityInputField != null)
        {
            velocityInputField.interactable = false;
        }

        if (nameInputField != null && massInputField != null && radiusInputField != null)
        {
            nameInputField.interactable = true;

            massInputField.interactable = true;

            radiusInputField.interactable = true;

            placeObjectButton.interactable = true;
        }
        EventSystem.current.SetSelectedGameObject(null);
    }

    /**
    * Handles the Track Cam button press event.
    **/
    public void OnTrackCamPressed()
    {
        instructionText.text =
    "<b>Track Cam Mode Activated!</b>\n\n" +
    "<b>CONTROLS:</b>\n" +
    "- Dropdown Menu: Select the tracked object.\n" +
    "- Right Mouse Button: Rotate the camera.\n" +
    "- Mousewheel: Zoom in/out.\n" +
    "- Time Scaler: Adjust time speed (Reset: 'R').\n" +
    "- Earth Cam Button: Toggle 'Earth Cam' or 'Satellite Cam'.\n" +
    "     * Earth Cam: Centers the view on Earth.\n" +
    "     * Satellite Cam: Centers the view on the selected satellite.\n\n" +
    "<b>MONITOR:</b> Altitude, Velocity, Apogee, Perigee\n\n" +
    "<b>THRUST:</b>\n" +
    "- Prograde / Retrograde: Speed up or slow down in orbit.\n" +
    "- Left / Right: Adjust lateral movement (changes inclination).\n" +
    "- Radial In / Radial Out: Thrust toward or away from the planet you're orbiting.\n\n" +
    "Monitor these values to observe orbital behaviors.\n" +
    "Switch to Free Cam to explore or place satellites.";


        ShowObjectPlacementPanel(false);
        ShowPanel(true);
        SetButtonState(freeCamButton, false);
        SetButtonState(trackCamButton, true);
        ShowThrustButtonsPanel(true);
        ShowApogeePerigeePanel(true);

        toggleOptionsPanel.SetActive(true);
        dropdown.SetActive(true);

        trackCamButton.interactable = false;
        freeCamButton.interactable = true;

        isTracking = true;

        if (velocityInputField != null)
        {
            velocityInputField.interactable = false;
        }

        if (nameInputField != null && massInputField != null && radiusInputField != null)
        {
            nameInputField.text = null;
            nameInputField.interactable = false;

            massInputField.text = null;
            massInputField.interactable = false;

            radiusInputField.text = null;
            radiusInputField.interactable = false;

            placeObjectButton.interactable = false;
        }
        EventSystem.current.SetSelectedGameObject(null);
    }

    /**
    * Switches the text for the UI Earth Btn/Satellite Btn
    **/
    public void OnEarthCamPressed()
    {
        if (earthCamPressed)
        {
            earthCamButtonText.text = "Satellite Cam";
            earthCamPressed = false;
        }
        else
        {
            earthCamButtonText.text = "Earth Cam";
            earthCamPressed = true;
        }
    }

    /**
    * Toggles the visibility of the object placement panel.
    * @param showObjectPlacementPanel - Displays the object placement panel on canvas
    * @param ShowThrustButtonsPanel - Displays the Thrust buttons panel on canvas
    * @param showApogeePerigeePanel - Displays the Apogee Perigee panel on canvas
    * @param showPanel - Displays the Velocity/Altitude panel on canvas
    **/
    public void ShowSelectPanels(bool showObjectPlacementPanel, bool showThrustButtonsPanel)
    {
        // If were tracking and any of the booleans are false
        if (!showObjectPlacementPanel)
        {
            toggleOptionsPanel.SetActive(false);
            freeCamButton.interactable = false;
            trackCamButton.interactable = false;
        }
        else
        {
            toggleOptionsPanel.SetActive(true);
            if (isTracking)
            {
                freeCamButton.interactable = true;
            }
            else
            {
                trackCamButton.interactable = true;
            }
        }
        if (!freeCamButton.interactable)
        {
            ShowObjectPlacementPanel(showObjectPlacementPanel);
        }
        ShowThrustButtonsPanel(showThrustButtonsPanel);
    }

    /**
    * Toggles the visibility of the object placement panel.
    * @param show - True to show the panel, false to hide it.
    **/
    private void ShowObjectPlacementPanel(bool show)
    {
        objectPlacementPanel.SetActive(show);
    }

    /**
    * Toggles the visibility of the object placement panel.
    * @param show - True to show the panel, false to hide it.
    **/
    private void ShowThrustButtonsPanel(bool show)
    {
        thrustButtons.SetActive(show);
    }

    /**
    * Toggles the visibility of the apogee and perigee panel.
    * @param show - True to show the panel, false to hide it.
    **/
    public void ShowApogeePerigeePanel(bool show)
    {
        apogeePerigeePanel.SetActive(show);
    }

    /**
    * Toggles the visibility of the general UI panel.
    * @param show - True to show the panel, false to hide it.
    **/
    private void ShowPanel(bool show)
    {
        objectInfoPanel.SetActive(show);
    }

    public void ShowFeedbackPanel()
    {
        showInstructionText = !showInstructionText;
        UpdateButtonText(); // Update the button text when toggling
        feedbackPanel.SetActive(showInstructionText);
        EventSystem.current.SetSelectedGameObject(null);
    }

    private void UpdateButtonText()
    {
        TMP_Text tmpButtonText = feedbackButton.GetComponentInChildren<TMP_Text>();
        if (tmpButtonText != null)
        {
            tmpButtonText.text = showInstructionText ? "Hide Instructions" : "Show Instructions";
        }
    }

    /**
    * Updates the visual state of a button.
    * @param button - The button to update.
    * @param isPressed - True if the button is active/pressed, false otherwise.
    **/
    private void SetButtonState(Button button, bool isPressed)
    {
        ColorBlock colors = button.colors;
        Color newColor;

        if (isPressed)
        {
            ColorUtility.TryParseHtmlString("#008CDB", out newColor); // Dark blue for active state.
        }
        else
        {
            ColorUtility.TryParseHtmlString("#008CDB", out newColor); // Purple for inactive state.
        }

        colors.normalColor = newColor;
        button.colors = colors;

        button.Select();
        button.OnDeselect(null); // Force the button to refresh its visual state.
    }



    public void UpdateOrbitUI(float apogee, float perigee, float semiMajorAxis, float eccentricity, float orbitalPeriod, float inclination, float RAAN)
    {
        SetText(apogeeText, "Apogee", apogee);
        SetText(perigeeText, "Perigee", perigee);
        SetText(semiMajorAxisText, "Semi-Major Axis", semiMajorAxis * 10f);
        SetText(eccentricityText, "Eccentricity", eccentricity, "", "F3");
        SetText(orbitalPeriodText, "Orbital Period", orbitalPeriod, "s");
        SetText(inclinationText, "Inclination", inclination, "°");
        SetText(raanText, "RAAN", RAAN, "°", "F1");
    }

    private void SetText(TextMeshProUGUI textElement, string label, float value, string unit = "km", string format = "F0")
    {
        if (textElement != null)
            textElement.text = value >= 0 ? $"{label}: {value.ToString(format)} {unit}".Trim() : string.Empty;
    }
}
