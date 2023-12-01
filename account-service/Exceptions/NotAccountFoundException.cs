using System;

public class NotAccountFoundException : Exception {
  public NotAccountFoundException(string message) : base(message) {}
}