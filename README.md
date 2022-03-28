# [Jmcarrasc0 - ActiveDirectory.Consult](https://www.patreon.com/join/jmcarrasc0?)

<a href="https://www.patreon.com/join/jmcarrasc0?"><img src="https://i.postimg.cc/hPcgmRWQ/jmcarrasc0-logo.png" alt="jmcarrasco logo" ></a>

**ActiveDirectory.Consult**  is a nuget package based on **.NET** for Active Directory user queries and application authentication.


## Quick install

Mail.Sending is constantly in development! Try it out now:

### NuGet

```sh
PM> Install-Package Jmcarrasc0.ActiveDirectory.Consult -Version 1.0.1
```

**or**

### .NET CLI

```sh
> dotnet add package Jmcarrasc0.ActiveDirectory.Consult --version 1.0.1
```

### PackageReference

```sh
<PackageReference Include="Jmcarrasc0.ActiveDirectory.Consult" Version="1.0.1" />
```
### Paket CLI

```sh
> paket add Jmcarrasc0.ActiveDirectory.Consult --version 1.0.1
```

### Script & Interactive

```sh
> #r "nuget: Jmcarrasc0.ActiveDirectory.Consult, 1.0.1"
```

### Cake

```sh
// Install Jmcarrasc0.ActiveDirectory.Consult as a Cake Addin
#addin nuget:?package=Jmcarrasc0.ActiveDirectory.Consult&version=1.0.1

// Install Jmcarrasc0.ActiveDirectory.Consult as a Cake Tool
#tool nuget:?package=Jmcarrasc0.ActiveDirectory.Consult&version=1.0.1
```

<br>
<br>

# How to use it

It is very easy to implement

## C#

```c#

using Jmcarrasc0.ActiveDirectory.Consult;

/// <summary>
/// Function to obtain data by user
/// </summary>
void GetUserActiveDirectorybyUsrName(string user, string ldap)
{
    var ad = new ActiveDirectory();

    var result = ad.GetUserActiveDirectorybyUsrName(user, ldap);
}

/// <summary>
/// Function to List all users in Active Directory
/// </summary>
void GetListUserActiveDirectory(string ldap)
{
    var ad = new ActiveDirectory();

    var result = ad.GetListUserActiveDirectory(ldap);
}

/// <summary>
/// Function to login with Active Directory
/// </summary>
void LoginActiveDirectory(string userName, string pass, string ldap, string domainName)
{
    var ad = new ActiveDirectory();

    var result = ad.LoginActiveDirectory(userName,pass,ldap,domainName);
}


```


<br>
<br>

## VB.NET

```vb

Imports Jmcarrasc0.ActiveDirectory.Consult;

'Function to obtain data by user

    Private Sub GetUserActiveDirectorybyUsrName(ByVal user As String, ByVal ldap As String)
        Dim ad = New ActiveDirectory()
        Dim result = ad.GetUserActiveDirectorybyUsrName(user, ldap)
    End Sub

'Function to List all users in Active Directory

    Private Sub GetListUserActiveDirectory(ByVal ldap As String)
        Dim ad = New ActiveDirectory()
        Dim result = ad.GetListUserActiveDirectory(ldap)
    End Sub

'Function to login with Active Directory

    Private Sub LoginActiveDirectory(ByVal userName As String, ByVal pass As String, ByVal ldap As String, ByVal domainName As String)
        Dim ad = New ActiveDirectory()
        Dim result = ad.LoginActiveDirectory(userName, pass, ldap, domainName)
    End Sub
        
```

<br>
<br>

## Compatibily Support

**Mail.Sending** is a nuget package compatible with the following Frameworks

- .NET 6
- .NET 5
- .NET Core 3.1


<br>
<br>




## Copyright and license ![Github](https://img.shields.io/github/license/jmcarrasc0/ActiveDirectory.Consult)

Code copyright 2022 Juan Carrasco. Code released under [the MIT license](https://github.com/jmcarrasc0/ActiveDirectory.Consult/blob/master/LICENSE).
