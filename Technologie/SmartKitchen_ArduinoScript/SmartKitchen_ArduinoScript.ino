  #include <SoftwareSerial.h>

  SoftwareSerial SoftSerial(0,1);
  
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
    for (int i; i < 4; i++)
    {
    pinMode(buttons[i], INPUT);
    digitalWrite(buttons[i], HIGH);
    }
    //+6+666
    +++++Serial.begin(9600);

    SoftSerial.begin(9600);
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

   if (currentDirection != lastDirection || (millis() - lastSendTime >= sendInterval && currentDirection != CENTER)) {
    String message;
    switch (currentDirection) {
      case UP:
        message = "Up";
        break;
      case DOWN:
        message = "Down";
        break;
      case LEFT:
        message = "Left";
        break;
      case RIGHT:
        message = "Right";
        break;
      case CENTER:
        message = "Center";
        break;
    }

    // Send the message over Bluetooth
    SoftSerial.println(message);
    //Serial.println(message); // Also print to Serial Monitor for debugging

    lastSendTime = millis();
    lastDirection = currentDirection;
  }

  delay(10);
}
