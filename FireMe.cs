using FireSharp.Interfaces;
using FireSharp.Config;
using FireSharp;
using System;
using System.Net;

namespace Product_Key_Getter
{
    //https://github.com/ziyasal/FireSharp/issues/73 
    //https://console.firebase.google.com/project/myfirekeys/database/myfirekeys/data
    //https://www.web2generators.com/html-based-tools/online-html-entities-encoder-and-decoder
    //https://github.com/iaaqib/To-Do-List-using-FireSharp/blob/master/To%20Do%20List%20with%20Firebase/MainPage.xaml.cs
    //https://kodesnippets.com/making-a-simple-to-do-list-with-firebase-using-firesharp/
    //https://www.youtube.com/watch?v=_jB2iOgo_9Q

    public class FireMe
    {
        private IFirebaseConfig config = new FirebaseConfig{
            AuthSecret = "sKlJD0bNG2CMSAUJwDBw3zf5dY6HVDQNH4W0ShA1",
            BasePath = "https://myfirekeys.firebaseio.com"
        };
        static IFirebaseClient client;

        public async void SetDataToFirebase(string[] dtat){
            //Initializing FirebaseClient with reference link
            client = new FirebaseClient(config);
            var winInfo = new WinInf
            {
                winId = dtat[0],
                winK = dtat[1],
                winVer = dtat[2],
                winUser = dtat[3],
            };
            var response = await client.PushAsync("/keys/newWindows", winInfo);
            Console.WriteLine(response);
        }

        public bool CheckForInternetConnection(){
            try{
                using (var client = new WebClient())
                using (client.OpenRead("http://google.com/"))
                    return true;
            }
            catch{
                return false;
            }
        }
    }
}
