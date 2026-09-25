## 2. Refleksion over SQL injection
Jeg bruger parameteriserede queries med Dapper, så brugerinput ikke bliver sat direkte ind i SQL-strengen. Det betyder, at input bliver behandlet som data og ikke som SQL-kode. På den måde beskytter jeg databasen mod SQL injection.

## 3. Refleksion over rettighedsstyring
Jeg har lavet en simpel rolleopdeling, hvor alle brugere kan oprette, læse og opdatere data, men kun en admin må slette. Det giver en form for least privilege, fordi almindelige brugere ikke får adgang til at fjerne data. Det beskytter databasen mod uautoriserede sletninger.

## 4. Refleksion over validering
Jeg validerer brugerinput i programmet, før det bliver sendt til databasen. Det er vigtigt, fordi det mindsker risikoen for ugyldige data og fejl i databasen. Samtidig sikrer constraints som foreign keys og unique values, at data stadig overholder reglerne i databasen.

## 5. Refleksion over stored procedures
Jeg har valgt at lægge en del af logikken i databasen som en stored procedure, så den kan ændres uden at ændre selve programkoden. Den modtager parametre, opdaterer data og indeholder validering af input. Det gør løsningen mere fleksibel og sikker.

## 6. Refleksion over fejlhåndtering
Jeg bruger try/catch omkring databasekald, så fejl kan håndteres pænt i stedet for at programmet crasher. Det gør det muligt at vise en meningsfuld fejlbesked til brugeren. Det er vigtigt, fordi databasefejl kan opstå ved fx ugyldige data, manglende forbindelser eller constraint-fejl. Nogle steder mangler jeg dog flere try/catch til fejlhåndtering(tiden løb fra mig....)