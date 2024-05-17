// See https://aka.ms/new-console-template for more information
using Newtonsoft.Json;

Console.WriteLine("Hello, World!");

string jsonstr = await File.ReadAllTextAsync("data.json");
var model = JsonConvert.DeserializeObject<MainDto>(jsonstr);
//Console.WriteLine(jsonstr);

foreach (var question in model.questions)
{
    Console.WriteLine(question.questionNo);
}

string BRJsonstr = await File.ReadAllTextAsync("BurmeseRecipesdata.json");
//string BRJsonstr = "{\r\n    \"Guid\": \"83b8aaf2-a81e-448a-a080-6fdb5b848cfc\",\r\n    \"Name\": \"ဝက်သုံးထပ်သားနှင့် ငုံးဥအချိုချက်\",\r\n    \"Ingredients\": \"ဝက်သား\\nငုံးဥ\\nကြက်သွန်ဖြူ\\nကြက်သွန်နီ\\nဂျင်း\\nနံနံပင်\\nကြက်သွန်မိတ်\\nခရုဆီ\\nပဲငံပြာရည်အနောက်\\nကြက်သားမှုန့်\\nသကြား\\nတရုတ်မဆလာ\",\r\n    \"CookingInstructions\": \"၁။ လုံးချက်တဲ့ပုံစံလေးပါ။ ဝက်သုံးထပ်သားကို ခရုဆီ၊ ပဲငံပြာရည်အနောက်၊ ကြက်သားမှုန့်၊ သကြားနဲ့ ထောင်းထားသောဂျင်း တို့ဖြင့် နယ်ထားပြီး အရသာဝင်အောင်နပ်ထားပေးပါ။\\n၂။ ငုံးဥကို ကြိုပြုတ်ထားပြီး အခွံနွှာထားပါ။\\n၃။ ညက်နေအောင်ထောင်းထားသော ကြက်သွန်ဖြူ/နီကို ဆီသတ်လို့ အနံ့မွှေးပြီဆို ဝက်သုံးထပ်သားထည့်၊ ရေနည်းငယ်ထည့် အုပ်ထားလိုက်ပါ။\\n၄။ (၂)ရေလောက်ခမ်းပြီဆိုမှ ကြက်သားမှုန့်အနည်းငယ်၊ တရုတ်မဆလာနဲ့ ငုံးဥထည့်ပြီး ရေအနည်းငယ်ထပ်ထည့်ပြီး အုပ်ထားလိုက်ပါ။\\n၅။ ဟင်းအိုးဆူပြီး ဝက်သားလည်းနူးအိပြီဆိုရင် နံနံပင်နဲ့ ကြက်သွန်မြိတ်အုပ်ပြီး အဆင်သင့်စားလို့ရပါပြီ။\\nငုံးဥနေရာမှာ ကြက်ဥတို့၊ ဘဲဥတို့နဲ့ ချက်လည်း ရပါတယ်။ ငရုတ်သီးအစပ်ကြိုက်ရင် အိုးချခါနီးငရုတ်သီးလေးတွေ ပါးပါးလှီးထည့်ပေးပါ။\\nမိမိနှင့် မိမိ၏ မိသားစုကျန်းမာရေးအတွက် အချိုမှုန့်လျော့သုံးပေးပါ။\",\r\n    \"UserType\": \"001\"\r\n  }";
var BRmodel = JsonConvert.DeserializeObject<List<BRMainDto>>(BRJsonstr);
Console.WriteLine(BRmodel);

Console.ReadLine();
static string ToNumber(string num)
{
    num = num.Replace("၁", "1");
    num = num.Replace("၂", "2");
    num = num.Replace("၃", "3");
    num = num.Replace("၄", "4");
    num = num.Replace("၅", "5");
    num = num.Replace("၆", "6");
    num = num.Replace("၇", "7");
    num = num.Replace("၈", "8");
    num = num.Replace("၉", "9");
    num = num.Replace("၁၀", "10");

    return num;
}
public class MainDto
{
    public Question[] questions { get; set; }
    public Answer[] answers { get; set; }
    public string[] numberList { get; set; }
}

public class Question
{
    public int questionNo { get; set; }
    public string questionName { get; set; }
}

public class Answer
{
    public int questionNo { get; set; }
    public int answerNo { get; set; }
    public string answerResult { get; set; }
}


//public class BRMainDto
//{
//    public Class1[] Property1 { get; set; }
//}

public class BRMainDto
{
    public string Guid { get; set; }
    public string Name { get; set; }
    public string Ingredients { get; set; }
    public string CookingInstructions { get; set; }
    public string UserType { get; set; }
}

public class BurmeseRecipes1
{
    public string Name { get; set; }
    public string Description { get; set; }
}
