using System;

[Serializable]
public class RobotMovementResponseDTO {

    public PositionDTO position;

    public RobotMovementResponseDTO (PositionDTO position) {
        this.position = position;
    }
}
