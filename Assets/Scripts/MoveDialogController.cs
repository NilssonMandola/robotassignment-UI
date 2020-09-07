using TMPro;
using UnityEngine;

public class MoveDialogController : MonoBehaviour {

    public GameObject moveDialog;
    public TMP_InputField positionInput;
    public TMP_InputField matrixSizeInput;
    public TMP_Dropdown directionInput;
    public TMP_InputField instructionInput;
    public TextMeshProUGUI robotText;

    private MoveDialogModel moveDialogModel;

    public void Start ( ) {
        moveDialogModel = new MoveDialogModel (positionInput, matrixSizeInput, directionInput, instructionInput, robotText);
    }

    public void openDialog ( ) {
        moveDialog.SetActive (true);
    }

    public void closeDialog ( ) {
        moveDialog.SetActive (false);
    }

    public void moveRobot ( ) {
        StartCoroutine(moveDialogModel.postToBackend ( ));
        closeDialog ( );
    }
}
