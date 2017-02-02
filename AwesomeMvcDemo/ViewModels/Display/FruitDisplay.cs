using Omu.AwesomeMvc;

namespace AwesomeMvcDemo.ViewModels.Display
{
    public class FruitDisplay : KeyContent
    {
        public FruitDisplay(object key, string content, string url)
            : base(key, content)
        {
            this.url = url;
        }

        public string url { get; set; }
    }
}