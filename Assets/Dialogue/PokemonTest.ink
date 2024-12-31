INCLUDE GlobalScript.ink

{pokemon_name == "" : -> main | ->already_choose} 
-> main

=== main ===
Which Pokemon you want to use ? #speaker:Mr_Black #layout:right #portrait:Mr_Black_Neutral
    +[Charmander]
        ->chosen ("Charmander")
    +[Bulbasaur]
        -> chosen ("Bulbasaur")
    +[Squirtle]
        -> chosen ("Squirtle")
        

===chosen(pokemon)===
~ pokemon_name = pokemon
You choose {pokemon}!
->END

=== already_choose ===
You already choose {pokemon_name } !#speaker:Mr_Black #layout:right #portrait:Mr_Black_Neutral
->END