using Epsilon;
[assembly: RegisterVirtualInput("Jump")]
[assembly: RegisterVirtualInput("Right")]
[assembly: RegisterVirtualInput("Left")]

[assembly: DefaultInputBinding("Space", "Jump")]
[assembly: DefaultInputBinding("D", "Right")]
[assembly: DefaultInputBinding("A", "Left")]