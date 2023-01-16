    public interface ICommand
    {
        void Execute();
        void Undo();
    }
    public class NoCommand : ICommand
    {
        public void Execute()
        {

        }
        
        public void Undo()
        {

        }
    }
    public class RemoteControl
    {
        ICommand[] OnCommands;
        ICommand[] OffCommands;
        ICommand undoCommand;
        public RemoteControl()
        {
            OnCommands = new ICommand[7];
            OffCommands = new ICommand[7];
            ICommand noCommand = new NoCommand();
            for (int i = 0; i < 7; i++)
            {
                OnCommands[i] = noCommand;
                OffCommands[i] = noCommand;
            }
            this.undoCommand = noCommand;
        }

        public void SetCommand(int slot, ICommand onCommand, ICommand offCommand)
        {
            this.OnCommands[slot] = onCommand;
            this.OffCommands[slot] = offCommand;
        }

        public void OnButtonWasPushed(int slot)
        {
            this.OnCommands[slot].Execute();
            this.undoCommand = OnCommands[slot];
        }

        public void OffButtonWasPushd(int slot)
        {
            this.OffCommands[slot].Execute();
            this.undoCommand = OffCommands[slot];
        }

        public void UndoButtonWasPushed()
        {
            this.undoCommand.Undo();               
        }
        public override String ToString()
        {
            StringBuilder stringBuilder = new StringBuilder();
            stringBuilder.Append("\n-------- 리모컨 -------\n");
            for (int i = 0; i < OnCommands.Length; i++)
            {
                stringBuilder.Append($"[slot {i}]" + this.OnCommands[i] + "  " + OffCommands[i] + "\n");
            }
            return stringBuilder.ToString();
        }
    }






    public class Light
    {
        public void On()
        {
            Console.WriteLine("조명 킴");
        }
        public void Off()
        {
            Console.WriteLine("조명 끔");
        }
    }
    public class GarageDoor
    {
        public void Up()
        {
            Console.WriteLine("문열어!");
        }
        public void Down()
        {
            Console.WriteLine("문닫아!");
        }
        public void Stop()
        {

        }
        public void LightOn()
        {

        }
        public void LightOff()
        {

        }

    }

    public class Stereo
    {
        public void On()
        {
            Console.WriteLine("노래 켜!");
        }
        public void Off()
        {
            Console.WriteLine("노래 꺼");
        }
        public void SetCd()
        {

        }
        public void SetDvd()
        {

        }
        public void SetRadio()
        {

        }
        public void SetVolume(int i)
        {

        }
    }

    public class SimpleRemoteControl
    {
        private ICommand slot;
        public SimpleRemoteControl()
        {

        }
        public void SetCommand(ICommand command)
        {
            this.slot = command;
            ;
        }
        public void ButtonWasPressed()
        {
            this.slot.Execute();
        }
    }


    public class LightOnCommand : ICommand
    {
        private Light light;
        public LightOnCommand(Light light)
        {
            this.light = light;
        }
        public void Execute()
        {
            this.light.On();
        }
        public void Undo()
        {
            this.light.Off();
        }
    }

    public class LightOffCommand : ICommand
    {
        private Light light;
        public LightOffCommand(Light light)
        {
            this.light = light;
        }
        public void Execute()
        {
            this.light.Off();
        }
        public void Undo()
        {
            this.light.On();
        }
    }

    public class StereoOnWithCdCommand : ICommand
    {
        private Stereo stereo;
        public StereoOnWithCdCommand(Stereo stereo)
        {
            this.stereo = stereo;
        }
        public void Execute()
        {
            stereo.On();
        }
        public void Undo()
        {
            stereo.Off();
        }
    }
    public class StereoOffWithCdCommand : ICommand
    {
        private Stereo stereo;
        public StereoOffWithCdCommand(Stereo stereo)
        {
            this.stereo = stereo;
        }
        public void Execute()
        {
            stereo.Off();
        }
        public void Undo()
        {
            stereo.On();
        }
    }

    public class GarageDoorOpenCommand : ICommand
    {
        private GarageDoor garageDoor;
        public GarageDoorOpenCommand(GarageDoor garageDoor)
        {
            this.garageDoor = garageDoor;
        }
        public void Execute()
        {
            this.garageDoor.Up();
        }
        public void Undo()
        {
            this.garageDoor.Down();
        }
    }
    public class GarageDoorCloseCommand : ICommand
    {
        private GarageDoor garageDoor;
        public GarageDoorCloseCommand(GarageDoor garageDoor)
        {
            this.garageDoor = garageDoor;
        }
        public void Execute()
        {
            this.garageDoor.Down();
        }
        public void Undo()
        {
            this.garageDoor.Up();
        }
    }



    class Program
    {
        static void Main(string[] args)
        {
            RemoteControl remoteControl = new RemoteControl();

            Light livingRoomLight = new Light();
            Light kitchenLight = new Light();
            GarageDoor garageDoor = new GarageDoor();
            Stereo stereo = new Stereo();

            LightOnCommand livingRoomLightOn = new LightOnCommand(livingRoomLight);
            LightOffCommand livingRoomLightOff = new LightOffCommand(livingRoomLight);
            
            LightOnCommand kitchenRoomLightOn = new LightOnCommand(kitchenLight);
            LightOffCommand kitchenRommLightOff = new LightOffCommand(kitchenLight);

            GarageDoorOpenCommand garageDoorOpen = new GarageDoorOpenCommand(garageDoor);
            GarageDoorCloseCommand garageDoorClose = new GarageDoorCloseCommand(garageDoor);

            StereoOnWithCdCommand stereoOnWithCd = new StereoOnWithCdCommand(stereo);
            StereoOffWithCdCommand stereoOffWithCd = new StereoOffWithCdCommand(stereo);

            remoteControl.SetCommand(0, livingRoomLightOn, livingRoomLightOff);
            remoteControl.SetCommand(1, kitchenRoomLightOn, kitchenRommLightOff);
            remoteControl.SetCommand(2, garageDoorOpen, garageDoorClose);
            remoteControl.SetCommand(3, stereoOnWithCd, stereoOffWithCd);

            Console.WriteLine(remoteControl);

            remoteControl.OnButtonWasPushed(0);
            remoteControl.OffButtonWasPushd(0);

            remoteControl.OnButtonWasPushed(1);
            remoteControl.OffButtonWasPushd(1);

            remoteControl.OnButtonWasPushed(2);
            remoteControl.OffButtonWasPushd(2);

            remoteControl.OnButtonWasPushed(3);
            remoteControl.OffButtonWasPushd(3);

            remoteControl.UndoButtonWasPushed();


        }
    }
