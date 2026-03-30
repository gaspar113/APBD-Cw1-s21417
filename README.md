# APBD-Cw1-s21417

## Instrukcja uruchomienia

```bash
dotnet run
```

## Podział kodu i uzasadnienie

Projekt to system wypożyczania sprzętu (laptopy, kamery, projektory) dla studentów i pracowników.

### Struktura

- **Models/** — klasy danych: `Device` (abstrakcyjna baza z `Id`, `Name`, `Status`) i jego podklasy (`Laptop`, `Camera`, `Projector`), `User` (baza z limitem wypożyczeń) i podklasy (`Student`, `Employee`), oraz `Loan` łączący użytkownika z urządzeniem.
- **Services/** — logika biznesowa, każdy serwis odpowiada za jedną domenę:
  - `DeviceService` — zarządzanie urządzeniami (dodawanie, zmiana statusu).
  - `UserService` — zarządzanie użytkownikami.
  - `LoanService` — obsługa wypożyczeń i zwrotów, walidacja limitów i dostępności.
  - `ReportService` — generowanie raportów (zestawienia sprzętu, zaległości, podsumowanie systemu).
- **Policies/** — interfejs `IPenaltyCalculator` i jego implementacja `DailyPenaltyCalculator`. Wydzielone osobno, bo sposób naliczania kar to decyzja biznesowa, która może się zmienić niezależnie od reszty logiki.
- **Enums/** — `DeviceStatus` (Available, Borrowed, Unavailable).

### Dlaczego taki podział?

Głównym kryterium była **spójność (cohesion)** — każda klasa i każdy serwis zajmuje się jedną, konkretną rzeczą:

- `LoanService` wie, jak wypożyczyć i zwrócić sprzęt, ale nie wie, jak wyświetlić raport — to robi `ReportService`.
- `ReportService` korzysta z danych trzech serwisów, ale sam nie modyfikuje stanu systemu — tylko czyta i formatuje.
- Naliczanie kar jest za interfejsem `IPenaltyCalculator`, więc zmiana polityki kar (np. z dziennej na progową) nie wymaga modyfikacji `LoanService` — wystarczy nowa implementacja interfejsu.
- Modele `Student` i `Employee` różnią się limitem wypożyczeń (`MaxActiveLoans`), co pozwala `LoanService` traktować wszystkich użytkowników jednolicie przez typ bazowy `User`, bez rozróżniania na `if/else`.

Dzięki temu poszczególne klasy można testować i modyfikować w izolacji — zmiana formatu raportu nie wpływa na logikę wypożyczeń, a dodanie nowego typu urządzenia wymaga tylko nowej podklasy `Device`.
