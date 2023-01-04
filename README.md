# WebAppLearn

Suivi du projet :

18/12/2022 : 
- Mise en place de l'environnement
- Compréhension :
  - Structure du projet
  - Routage
  - Razor

19-20/12/2022 :
- Mise en place d'une bdd : 
  - Explorateur d'objets SQL server
  - Fichier appsettings.json
- NuGet :
  - Modification de la source de package
  - Package System.Data.SqlClient
  - Package Microsoft.EntityFrameworkCore
- Première requete sql "inscription"

03/01/2023 :
- Utilisation du dictionnaire "ViewData", qui permet de passer des
données entre le contrôleur et la vue.

- refonte des views en utilisant correctement le moteur de template Razor.

Modifications de la requete sql :
- ajout de la préparation de la requete
- vérification de l'adresse mail pour ne pas créer plusieurs comptes avec la même adresse mail.
- asp-validation-for utilisé lors de l'inscription

04/01/2023 :
- Réalisation de la connexion
- documentation hashage
- Ajout de hashage de mot de passe (Pbkdf2, déconseillé dans la doc, mauvaise pratique)
- Modification du hashage pour Identity (bonne pratique)
- documentation session
- documentation routage
