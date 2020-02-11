# Sklep komputerowy

1. Uzytkownik moze byc klientem lub pracownikiem sklepu
2. Klient sam sie rejestruje przed zlozeniem zamowienia
3. Dodawanie, edycja produkt�w (Nazwa, Cena, Ilosc)
4. Tworzenie, edycja zam�wie� przez klient�w
5. Akceptacja, edycja zam�wie� przez pracownik�w

## Opis dzia�ania

1. Logowanie 
- Nowy klient rejestruje si� w systemie przyciskiem rejestracji
  - Rejestracja umo�liwia dodanie nowego u�ytkownika i danych jego konta do logowania
- Zarejestrowany u�ytkownik loguje si� loginem i has�em ze swojej rejestracji
- Zamkni�cie okna logowania powoduje zamkni�cie programu

2. Okno sklepu komputerowgo (zalogowany klient)
- Wyloguj
- Przycisk Edytuj konto - edycja konta zalogowanego u�ytkownika
- Od�wie� - od�wie�a list� zam�wie� zalogowanego klienta
- Dodaj zam�wienie - otwarcie formularza dodawania zam�wienia
- Edytuj (na rekordzie zam�wienia w li�cie zam�wie�) - edycja zam�wienia, je�eli to nie zosta�o zrealizowane przez pracownika

3. Okno sklepu komputerowego (zalogowany pracownik, login i has�o: j)
- Wyloguj
- Edytuj konto (j/w)
- Od�wie� (j/w)
- Lista klient�w - wyb�r klienta, kt�rego zam�wienia s� widoczne (konieczne klikni�cie przycisku Od�wie� po zmianie klienta)
- Edytuj (na rekordzie zam�wienia w li�cie zam�wie�) - edycja zam�wienia, je�eli to nie zosta�o zrealizowane przez pracownika
- Zrealizowane (na rekordzie zam�wienia w li�cie zam�wie�) - zmiana statusu zam�wienia na Zrealizowane, je�eli to nie zosta�o zrealizowane przez pracownika
  - Realizacja zam�wienia wi��e si� z rezerwacj� towaru w magazynie, wi�c po zmianie statusu towar jest odejmowany z magazynu (zmniejsza si� ilo�c sztuk produktu w magazynie)
- Magazyn - okno magazynu produkt�w

4. Edycja konta
- Formularz umo�liwia edycj� danych klienta (nazwa, adres, telefon, email) oraz danych konta (login, has�o)

5. Dodawanie zam�wienia (zalogowany klient)
- Zam�wienie sk�ada si� z tytu�u, osoby zamawiaj�cej (zalogowanego klienta), produkt�w w zam�wieniu.
- Lista produkt�w do wyboru jest konfigurowana przez pracownika. 
- Dodawanie produktu do zam�wienia
  - Dodanie produktu do zam�wienia odbywa si� przez zaznaczenie danego produktu na li�cie po prawej stronie i klikni�cie przycisku Dodaj pomi�dzy listami.
  - Po klikni�ciu Dodaj mo�na otrzyma� informacj� o braku towaru w magazynie, je�eli ilo�� towar�w w magazynie jest zerowa.
  - Podczas dodawania produktu nale�y wpisa� w specjalne pole ilo�� zamawianych produkt�w.
- Usuwanie produktu z zam�wienia odbywa si� przez klikni�cie danego produktu na li�cie, a nast�pnie klikni�cie przycisku Usu� pomi�dzy listami
- Dodane zam�wienie otrzymuje status Nowe.
- Po dodaniu zam�wienia nale�y zaczekac na realizacj� zam�wienia przez Pracownika. Do tego czasu mo�na je edytowa�.

6. Magazyn (zalogowany pracownik) 
- Okno wy�wietla list� produkt�w dost�pnych w systemie i ich g��wne dane (kategoria, nazwa, cena ilo�� w magazynie)
- Przycisk Dodaj produkt
- Edycja produktu - uruchamia si� po klikni�ciu na rekord
- Usuwanie produktu - usuwa produkt i wszystkie jego zale�no�ci w systemie

7. Dodawanie produkt�w (zalogowany pracownik, z poziomu magazynu)
- Okno dodawania produktu umo�liwia dodawanie nowego produktu do systemu i jego danych (nazwa, kategoria - wyb�r z listy dost�pnycn w systemie, ilosc, cena za 1 sztuk�)

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