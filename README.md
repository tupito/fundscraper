# Pikaohje tarkastajalle

* kloonaa repositorio
* asenna nuget-paketit
* /bin/debug/input.json pitää olla olemassa

## seuraavat Nuget-paketit pitää asentaa

* (Visual Studio) -> Tools -> NuGet Package Manager -> Package Manager Console
  * Install-Package HtmlAgilityPack
  * Install-Package Newtonsoft.Json

## jos jostain syystä /bin/debug/input.json -tiedostoa ei ole
* avaa projekti visual studiolla -> luo /bin/debug-kansion
* kopioi projektin juuresta input.json /bin/debug-kansioon