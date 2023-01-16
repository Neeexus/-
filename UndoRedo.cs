  public abstract class Command
    {
        public abstract void Execute();
        public abstract void UnExecute();
    }


    public class CalculatorCommand : Command
    {
        char op;    // 연산자
        int operand;    // 피연산자
        Calculator calculator;

        public CalculatorCommand(Calculator calculator, char op, int operand)
        {
            this.calculator = calculator;
            this.op = op;
            this.operand = operand;
        }


        public override void Execute()
        {
            calculator.Operation(op, operand);
        }
        
        public override void UnExecute()
        {
            calculator.Operation(Undo(op), operand);
        }
        // 반대되는 operator 리턴
        private char Undo(char op)
        {
            switch (op)
            {
                case '+': return '-';
                case '-': return '+';
                case '*': return '/';
                case '/': return '*';
                default: throw new Exception("op");
            }
        }
    }

    public class Calculator
    {
        int curr = 0;
        public void Operation(char op, int operand)
        {
            switch (op)
            {
                case '+': curr += operand; break;
                case '-': curr -= operand; break;
                case '*': curr *= operand; break;
                case '/': curr /= operand; break;
            }
            Console.WriteLine("현재 값 = {0} (계산식: {1} {2})", curr, op, operand);
        }
    }

    public class User
    {
        Calculator calculator = new Calculator();
        List<Command> commands = new List<Command>();
        int current = 0;
        public void Redo(int levels)
        {
            Console.WriteLine("\nRedo {0}", levels);

            for (int i = 0; i < levels; i++)
            {
                if (current < commands.Count - 1)
                {
                    Command command = commands[current++];
                    command.Execute();
                }
            }
        }
        public void Undo(int levels)
        {
            Console.WriteLine("\nUndo {0}", levels);

            for (int i = 0; i < levels; i++)
            {
                if (current > 0)
                {
                    Command command = commands[--current] as Command;
                    command.UnExecute();
                }
            }
        }
        public void Compute(char op, int operand)
        {
            Command command = new CalculatorCommand(calculator, op, operand);
            command.Execute();
            // 커맨드를 undo 리스트에 추가
            commands.Add(command);
            current++;
        }
    }


    public class Program
    {
        public static void Main(string[] args)
        {
            User user = new User();

            user.Compute('+', 100);
            user.Compute('-', 50);
            user.Compute('*', 10);
            user.Compute('/', 2);
            // Undo 4 
            user.Undo(4);

            // Redo 3 
            user.Redo(3);


        }
    }
