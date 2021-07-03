function Login()
{
    let password = document.getElementById("pass1").value;
    if(password.length < 5)
    {
        WarnungSetzen("Das Passwort muss mind. 5 Zeichen lang sein!");
    }
    else
    {
        mp.trigger('Auth.Login', password);
    }
}

function Register()
{
    let password = document.getElementById("pass1").value;
    let password2 = document.getElementById("pass2").value;
    if(password.length < 5 || password2.length < 5)
    {
        WarnungSetzen("Das Passwort muss mind. 5 Zeichen lang sein!");
    }
    else
    {
        if(password != password2)
        {
            WarnungSetzen("Die Passwörter stimmen nicht überein!");
        }
        else
        {
            mp.trigger('Auth.Register', password);
        }
    }
}

function WarnungSetzen(text)
{
    if(text.length > 0)
    {
        document.getElementById('warning').innerHTML = text;
    }
}