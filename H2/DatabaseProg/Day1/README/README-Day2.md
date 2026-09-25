## 2. Seeding function
Jeg lavede en seeding-funktion, så databasen hurtigt kunne få testdata og dermed bruges til de næste øvelser. Det gjorde det lettere at teste CRUD, relationer og senere sikkerhed og validering. Seed-dataene blev kun indsat, hvis databasen var tom, så jeg undgik dubletter.

## 3. Forretningsregler
Jeg har lavet forretningsregler som en separat klasse, så reglerne ikke ligger direkte i databasekoden. Det blev lidt påvirket af, at den første database ikke var bygget med helt løs kobling fra starten, så arkitekturen kan virke lidt mindre ren end planlagt. Reglerne hjælper stadig med at sikre, at data følger de krav, der giver mening i bilværkstedet.

## 4. Constraints
Jeg har brugt constraints som foreign keys, unique og not null til at sikre datakvalitet direkte i databasen. Det er nyttigt, fordi databasen selv afviser ugyldige værdier, også hvis de kommer fra et andet program. Jeg valgte fx unique på email og license plate, samt foreign keys mellem kunder, biler og værkstedsordrer.

## 5. Trigger
Jeg lavede en trigger på WorkOrders, som automatisk skriver ændringer til en logtabel, når en status bliver opdateret. Triggeren blev oprettet fra .NET, men selve automatiseringen sker i databasen.

## 6. Data historik / log
Jeg lavede også logning til en fil, så ændringer kan gemmes som historik udenfor databasen. Det gør det muligt at se, hvad der er sket, og det er nyttigt som dokumentation og sporbarhed. Logningen hjælper med at bevare overblik over ændringer og passer derfor godt ind i dataintegritet.