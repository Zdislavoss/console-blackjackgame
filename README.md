# Console Blackjack Game

Školní projekt do předmětu Programování.

Projekt simuluje karetní hru Blackjack. Hráč sází žetony, rozhoduje o svých tazích a snaží se porazit dealera podle klasických pravidel Blackjacku.


---

# Popis hry

Blackjack je karetní hra, ve které se hráč snaží získat součet karet co nejblíže hodnotě 21.

Ve hře jsou implementována základní pravidla Blackjacku:

- Dealer líže do 16 bodů a na 17 a více stojí.
- Eso má hodnotu 11 nebo 1 podle situace.
- Blackjack (eso + karta za 10 bodů) vyplácí 1,5 násobek sázky.
- Možnost zdvojnásobení sázky (Double).
- Možnost rozdělení dvojice stejných karet (Split).
- Statistiky odehraných her.

Podrobný popis pravidel:
[Pravidla Blackjacku](https://cs.wikipedia.org/wiki/Blackjack)
---

# Funkce

- Hra Blackjack proti dealerovi ovládanému počítačem
- Sázení žetonů
- Double
- Split
- Blackjack bonus
- Barevný výpis karet
- Statistiky odehraných her
- Validace vstupů
- Ošetření neplatných akcí

---

# Ovládání

Po spuštění hry hráč zadá sázku.

Během kola může používat následující příkazy:

| Příkaz | Význam |
|---------|---------|
| hit | líznout kartu |
| stand | ukončit tah |
| double | zdvojnásobit sázku a vzít poslední kartu |
| split | rozdělit dvě stejné karty do dvou ruk |

Po skončení kola se hráč rozhodne, zda chce pokračovat ve hře.

---

# Struktura projektu

## Card.cs

Reprezentuje jednu hrací kartu.

Obsahuje:

- barvu karty
- hodnotu karty
- blackjack hodnotu karty
- symbol karty

---

## Deck.cs

Spravuje balíček karet.

Obsahuje:

- vytvoření balíčku
- zamíchání balíčku
- dobírání karet

---

## Participant.cs

Abstraktní třída společná pro hráče a dealera.

Obsahuje:

- seznam karet v ruce
- přidávání karet
- mazání karet
- výpočet skóre
- výpis karet

---

## Player.cs

Reprezentuje hráče.

Obsahuje:

- počet žetonů
- aktuální sázku
- správu sázek
- výhry a remízy

---

## Dealer.cs

Reprezentuje dealera.

Obsahuje:

- pravidla dobírání karet
- zobrazení skryté karty
- automatické rozhodování podle pravidel Blackjacku

---

## GameManager.cs

Hlavní řídící třída celé hry.

Obsahuje:

- herní smyčku
- rozdávání karet
- vyhodnocení výsledků
- práci se sázkami
- systém Double
- systém Split
- statistiky

---

## Program.cs

Vstupní bod aplikace.

Spouští vytvoření GameManageru a následně celou hru.

---

# Použité OOP principy

## Dědičnost

Projekt využívá dědičnost mezi třídami:

```text
Participant
├── Player
└── Dealer
```

Player i Dealer dědí společné vlastnosti a metody ze třídy Participant.

---

## Abstraktní třída

Projekt využívá abstraktní třídu:

```text
Participant
```

Tato třída obsahuje společné chování pro hráče i dealera.

---

## Zapouzdření

V projektu jsou používány přístupové modifikátory:

- private
- protected
- public

Data nejsou ukládána do veřejných polí.

---

## Kolekce

Projekt využívá kolekci:

```csharp
List<Card>
```

pro ukládání karet v ruce hráče a dealera.

---

# Validace vstupů

Všechny vstupy od uživatele jsou kontrolovány.

Pro kontrolu číselných vstupů je použito:

```csharp
int.TryParse(...)
```

Díky tomu program nespadne při zadání neplatné hodnoty.

Kontrolují se například:

- sázky
- potvrzení pokračování hry
- herní příkazy

---

# Statistiky

Po ukončení hry se zobrazují statistiky:

- počet odehraných her
- počet výher hráče
- počet výher dealera
- počet remíz
- počet Blackjacků

---

# Vývoj projektu

## 27.05.2026

- vytvoření GitHub repozitáře
- propojení projektu s Riderem
- vytvoření základních tříd
- vytvoření README
- vytvoření .gitignore
- návrh struktury projektu

## 03.06.2026

- vytvoření a dokončení Card.cs
- vytvoření Deck.cs
- vytvoření Participant.cs
- návrh dědičnosti
- návrh základní logiky hry

## 08.06.2026

- vytvoření Player.cs
- vytvoření Dealer.cs
- implementace dědičnosti
- vytvoření GameManager.cs
- dokončení základní hratelné verze
- oprava zobrazování dealerových karet
- přidání barevného výpisu

## 12.06.2026

- dokončení verze V2
- přidání systému Double
- přidání systému Split
- přepracování GameManageru
- přesun některých metod do vhodnějších tříd
- odstranění duplicitního kódu
- úprava výpisů během hry
- doplnění komentářů
- finální testování projektu
- úprava README
- příprava projektu k odevzdání

---



# Použití AI

Při vývoji projektu byla využita umělá inteligence jako pomocný nástroj.

AI byla použita především pro:

- rozvržení projektu
- rady, při postupování projektu
- vysvětlení chyb v kódu
- návrhy možných řešení problémů
- konzultaci návrhu tříd
- refaktoring některých částí projektu
- vysvětlení fungování některých metod

Příklady použitých promptů:

- Jak navrhnout třídy pro Blackjack v C#?

- Jak implementovat Split v Blackjacku?
- Jak přesunout metody pro práci s kartami do Participant.cs, aby se neopakoval kód?
- Jak rozdělit moc dlouhou třídu GameManager na menší metody s jasnější odpovědností?
- Jak odstranit duplicitní metody pro výpis karet v konzoli?

Veškerý výsledný kód byl následně upraven, otestován a pochopen autorem projektu.

### Poznámka

Obsah README.md byl vizuálně upraven s pomocí AI. AI byla využita pouze pro návrh struktury dokumentace, formátování textu a přehlednější organizaci jednotlivých sekcí. Obsah dokumentace vychází z reálné implementace projektu.

---

# Spuštění projektu

Naklonování repozitáře:

```bash
git clone https://github.com/Zdislavoss/console-blackjackgame.git
```

Projekt lze otevřít v JetBrains Rideru nebo Microsoft Visual Studiu a následně spustit.

Po spuštění se zobrazí hlavní menu hry Blackjack.

---
# Autor

**Radek Pavelka**
