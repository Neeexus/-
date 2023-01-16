    public interface ICommand
    {
        void Execute();
    }

    class Program
    {
        static void Main(string[] args)
        {
            Lamp lamp = new Lamp();
            ICommand lampOnCommand = new Lamp.LampOnCommand(lamp);
            Alarm alarm = new Alarm();
            ICommand alarmStartCommand = new Alarm.AlarmStartCommand(alarm);

            Button button1 = new Button(lampOnCommand); // 램프 켜는 Command 설정
            button1.Pressed(); // 램프 켜는 기능 수행

            Button button2 = new Button(alarmStartCommand); // 알람 울리는 Command 설정
            button2.Pressed(); // 알람 울리는 기능 수행

            button2.SetCommand(lampOnCommand); // 다시 램프 켜는 Command로 설정
            button2.Pressed(); // 램프 켜는 기능 수행

        }
    }
    // 버튼은 안고침
    public class Button
    {
        private ICommand theCommand;

        public Button(ICommand theCommand) { 
            SetCommand(theCommand); 
        }

        public void SetCommand(ICommand newCommand) { 
            this.theCommand = newCommand; 
        }

        public void Pressed() { 
            this.theCommand.Execute(); 
        }

    }
    public class Lamp
    {
        public void turnOn()
        {
            Console.WriteLine("Lamp On");
        }
        public class LampOnCommand : ICommand
        {
            private Lamp theLamp;

            public LampOnCommand(Lamp theLamp)
            {
                this.theLamp = theLamp;
            }
            public void Execute()
            {
                this.theLamp.turnOn();
            }
        }
    }
    public class Alarm
    {
        public void start()
        {
            Console.WriteLine("Alarming");
        }
        public class AlarmStartCommand : ICommand
        {
            private Alarm theAlarm;

            public AlarmStartCommand(Alarm theAlarm)
            {
                this.theAlarm = theAlarm;
            }
            public void Execute()
            {
                this.theAlarm.start();
            }
        }
    }
