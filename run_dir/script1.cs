namespace Foo
{
    public class Bar
    {
        public bool SayHello(int idx)
        {
            System.Console.Write("Hello Omri");
            return idx%2==0?true:false;
        }
    }
}
