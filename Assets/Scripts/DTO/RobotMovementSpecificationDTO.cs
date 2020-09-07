using System;

[Serializable]
public class RobotMovementSpecificationDTO {

	public string movementInstructions;
	public string matrixSize;
	public PositionDTO initialPosition;

    public RobotMovementSpecificationDTO (string movementInstructions, string matrixSize, PositionDTO initialPosition) {
		this.movementInstructions = movementInstructions;
		this.matrixSize = matrixSize;
		this.initialPosition = initialPosition;
    }
}
