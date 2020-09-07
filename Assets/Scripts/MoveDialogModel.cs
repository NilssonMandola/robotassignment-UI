using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;

public class MoveDialogModel {

    private const string MOVE_ENDPOINT = "http://robotassignment-env.eba-pwnrcjyn.eu-central-1.elasticbeanstalk.com/move";

    private TMP_InputField positionInput;
    private TMP_InputField matrixSizeInput;
    private TMP_Dropdown directionInput;
    private TMP_InputField instructionInput;
    private TextMeshProUGUI robotText;

    private char [ ] positionInputArray;

    public MoveDialogModel (
        TMP_InputField positionInput,
        TMP_InputField matrixSizeInput,
        TMP_Dropdown directionInput,
        TMP_InputField instructionInput,
        TextMeshProUGUI robotText) {
        this.positionInput = positionInput;
        this.matrixSizeInput = matrixSizeInput;
        this.directionInput = directionInput;
        this.instructionInput = instructionInput;
        this.robotText = robotText;
    }

    public IEnumerator postToBackend ( ) {
        positionInputArray = positionInput.text.ToCharArray ( );
        if (validateInput ( )) {
            Handheld.StartActivityIndicator ( );
            PositionDTO positionDTO = new PositionDTO (positionInputArray [1].ToString ( ), positionInputArray [3].ToString ( ), getDirectionDTO (directionInput.value).ToString());
            RobotMovementSpecificationDTO robotMovementSpecificationDTO = new RobotMovementSpecificationDTO (instructionInput.text, matrixSizeInput.text, positionDTO);
            UnityWebRequest request = UnityWebRequest.Post (MOVE_ENDPOINT, "");
            request.uploadHandler = new UploadHandlerRaw (System.Text.Encoding.UTF8.GetBytes (JsonUtility.ToJson (robotMovementSpecificationDTO).Normalize ( )));
            request.SetRequestHeader ("Content-Type", "application/json");
            yield return request.SendWebRequest ();
            if (request.isNetworkError || request.isHttpError) {
                robotText.text = "Network error :(";
            } else {
                PositionDTO responsePositionDTO = JsonUtility.FromJson<RobotMovementResponseDTO> (request.downloadHandler.text).position;
                robotText.text = responsePositionDTO.xPosition + " " + responsePositionDTO.yPosition + " " + responsePositionDTO.direction;
            }
        } else {
            robotText.text = "Formatting error :(";
        }
        Handheld.StopActivityIndicator ( );
    }

    private bool validateInput ( ) {
        //FIXME this is error prone, like fourth of july for NPE
        return "(".Equals (positionInputArray [0].ToString ( )) &&
            isNumber (positionInputArray [1].ToString ( )) &&
            ",".Equals (positionInputArray [2].ToString ( )) &&
            isNumber (positionInputArray [3].ToString ( )) &&
            ")".Equals (positionInputArray [4].ToString ( ));
    }

    private bool isNumber (string str) {
        return int.TryParse (str, out _);
    }

    private DirectionDTO getDirectionDTO (int selected) {
        switch (selected) {
            case 0: return DirectionDTO.N;
            case 1: return DirectionDTO.E;
            case 2: return DirectionDTO.S;
            case 3: return DirectionDTO.W;
            default:
            robotText.text = "Formatting error :(";
            throw new NotImplementedException ( );
        }
    }

}
