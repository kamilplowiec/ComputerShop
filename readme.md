# Sklep komputerowy

1. Uzytkownik moze byc klientem lub pracownikiem sklepu
2. Klient sam sie rejestruje przed zlozeniem zamowienia
3. Dodawanie, edycja produktów (Nazwa, Cena, Ilosc)
4. Tworzenie, edycja zamówieñ przez klientów
5. Akceptacja, edycja zamówieñ przez pracowników

## Opis dzia³ania

1. Logowanie 
- Nowy klient rejestruje siê w systemie przyciskiem rejestracji
  - Rejestracja umo¿liwia dodanie nowego u¿ytkownika i danych jego konta do logowania
- Zarejestrowany u¿ytkownik loguje siê loginem i has³em ze swojej rejestracji
- Zamkniêcie okna logowania powoduje zamkniêcie programu

2. Okno sklepu komputerowgo (zalogowany klient)
- Wyloguj
- Przycisk Edytuj konto - edycja konta zalogowanego u¿ytkownika
- Odœwie¿ - odœwie¿a listê zamówieñ zalogowanego klienta
- Dodaj zamówienie - otwarcie formularza dodawania zamówienia
- Edytuj (na rekordzie zamówienia w liœcie zamówieñ) - edycja zamówienia, je¿eli to nie zosta³o zrealizowane przez pracownika

3. Okno sklepu komputerowego (zalogowany pracownik, login i has³o: j)
- Wyloguj
- Edytuj konto (j/w)
- Odœwie¿ (j/w)
- Lista klientów - wybór klienta, którego zamówienia s¹ widoczne (konieczne klikniêcie przycisku Odœwie¿ po zmianie klienta)
- Edytuj (na rekordzie zamówienia w liœcie zamówieñ) - edycja zamówienia, je¿eli to nie zosta³o zrealizowane przez pracownika
- Zrealizowane (na rekordzie zamówienia w liœcie zamówieñ) - zmiana statusu zamówienia na Zrealizowane, je¿eli to nie zosta³o zrealizowane przez pracownika
  - Realizacja zamówienia wi¹¿e siê z rezerwacj¹ towaru w magazynie, wiêc po zmianie statusu towar jest odejmowany z magazynu (zmniejsza siê iloœc sztuk produktu w magazynie)
- Magazyn - okno magazynu produktów

4. Edycja konta
- Formularz umo¿liwia edycjê danych klienta (nazwa, adres, telefon, email) oraz danych konta (login, has³o)

5. Dodawanie zamówienia (zalogowany klient)
- Zamówienie sk³ada siê z tytu³u, osoby zamawiaj¹cej (zalogowanego klienta), produktów w zamówieniu.
- Lista produktów do wyboru jest konfigurowana przez pracownika. 
- Dodawanie produktu do zamówienia
  - Dodanie produktu do zamówienia odbywa siê przez zaznaczenie danego produktu na liœcie po prawej stronie i klikniêcie przycisku Dodaj pomiêdzy listami.
  - Po klikniêciu Dodaj mo¿na otrzymaæ informacjê o braku towaru w magazynie, je¿eli iloœæ towarów w magazynie jest zerowa.
  - Podczas dodawania produktu nale¿y wpisaæ w specjalne pole iloœæ zamawianych produktów.
- Usuwanie produktu z zamówienia odbywa siê przez klikniêcie danego produktu na liœcie, a nastêpnie klikniêcie przycisku Usuñ pomiêdzy listami
- Dodane zamówienie otrzymuje status Nowe.
- Po dodaniu zamówienia nale¿y zaczekac na realizacjê zamówienia przez Pracownika. Do tego czasu mo¿na je edytowaæ.

6. Magazyn (zalogowany pracownik) 
- Okno wyœwietla listê produktów dostêpnych w systemie i ich g³ówne dane (kategoria, nazwa, cena iloœæ w magazynie)
- Przycisk Dodaj produkt
- Edycja produktu - uruchamia siê po klikniêciu na rekord
- Usuwanie produktu - usuwa produkt i wszystkie jego zale¿noœci w systemie

7. Dodawanie produktów (zalogowany pracownik, z poziomu magazynu)
- Okno dodawania produktu umo¿liwia dodawanie nowego produktu do systemu i jego danych (nazwa, kategoria - wybór z listy dostêpnycn w systemie, ilosc, cena za 1 sztukê)

## Tabele

1. Osoba
- id
- nazwa
- adres
- telefon
- email
- login
- haslo
- pracownik (bool)

2. Produkt
- id
- nazwa
- ilosc
- cena
- typ (kategoria)

3. Zamowienie
- id
- osoba_id
- tytul

4. ProduktZamowienia
- id
- zamowienie_id
- produkt_id
- ilosc