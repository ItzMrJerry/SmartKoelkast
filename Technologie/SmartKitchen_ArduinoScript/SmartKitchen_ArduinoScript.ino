  int joystick_axis_x = A0;
  int joystick_axis_y = A1;
  int up_button = 2;
  int down_button = 4;
  int left_button = 5;
  int right_button = 3;

  int buttons[] = {up_button, down_button, left_button, right_button};

  enum Direction {
    CENTER,
    UP,
    DOWN,
    LEFT,
    RIGHT
  };

  Direction currentDirection = CENTER;
  Direction lastDirection = CENTER;

  unsigned long lastSendTime = 0;
  int sendInterval = 500;


  int centerThreshold = 200; // center treshold value
  int xCenter = 350;
  int yCenter = 350;
  void setup() {
    for (int i; i < 7; i++)
    {
    pinMode(buttons[i], INPUT);
    digitalWrite(buttons[i], HIGH);
    }
    Serial.begin(9600);
  }

  void loop() {
  int xValue = analogRead(joystick_axis_x);
  int yValue = analogRead(joystick_axis_y);

  // Determine the joystick's direction based on analog values
  if (yValue > yCenter + centerThreshold) {
    currentDirection = UP;
  } else if (yValue < yCenter - centerThreshold) {
    currentDirection = DOWN;
  } else if (xValue > xCenter + centerThreshold) {
    currentDirection = RIGHT;
  } else if (xValue < xCenter - centerThreshold) {
    currentDirection = LEFT;
  } else {
    currentDirection = CENTER;
  }

  // Override joystick direction if a button is pressed
  if (digitalRead(up_button) == LOW) {
    currentDirection = UP;
  } else if (digitalRead(down_button) == LOW) {
    currentDirection = DOWN;
  } else if (digitalRead(left_button) == LOW) {
    currentDirection = LEFT;
  } else if (digitalRead(right_button) == LOW) {
    currentDirection = RIGHT;
  }

  // Check if the direction has changed or enough time has passed to send it again
  if (currentDirection != lastDirection || (millis() - lastSendTime >= sendInterval && currentDirection != CENTER)) {
    switch (currentDirection) {
      case UP:
        Serial.println("Up");
        break;
      case DOWN:
        Serial.println("Down");
        break;
      case LEFT:
        Serial.println("Left");
        break;
      case RIGHT:
        Serial.println("Right");
        break;
      case CENTER:
        Serial.println("Center");
        break;
    }
    lastSendTime = millis(); // Update the last send time
    lastDirection = currentDirection;
  }

  delay(10); // Small delay to stabilize readings
}
