# HUOM, tarkastajalle

* pikaohje, jotta saat ohjelman käyntiin:
  * kloonaa repositorio
  * avaa projekti visual studiolla -> luo /bin/debug-kansion
  * kopioi repositorion juuresta input.json /bin/debug-kansioon (ohjelma tarvitsee input.json:n)

## seuraavat Nuget-paketit pitää asentaa

* Install-Package HtmlAgilityPack
* Install-Package Newtonsoft.Json
* tämän jälkeen ohjelman pitäisi saada ajettua

## TODO

* fix input/output
* johonkin luokkaan testit