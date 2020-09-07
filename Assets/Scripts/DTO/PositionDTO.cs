using System;

[Serializable]
public class PositionDTO {

    public string xPosition;
    public string yPosition;
    public string direction;

    public PositionDTO (string xPosition, string yPosition, string direction) {
        this.xPosition = xPosition;
        this.yPosition = yPosition;
        this.direction = direction;

    }
}