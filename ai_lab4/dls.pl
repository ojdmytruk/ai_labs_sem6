solve_dfs(MaxDepth,State,[]):-
    MaxDepth > 0,
    final_state(State).
solve_dfs(MaxDepth,State,[Move|Moves]):-
    MaxDepth > 0,
    move(State,Move),
    update(State,Move,State1),
    MaxDepth1 is MaxDepth - 1,
    solve_dfs(MaxDepth1,State1,Moves).

% capacity( номер_посудини, ємність_у_літрах)

% ємність першої посудини (бідона) - 5 л
capacity(1,5).
% ємність другої посудини (банка) - 3 л
capacity(2,3).

% Фінальний стан
final_state(jugs(4,_)).

move(jugs(_,_),fill(1)).
move(jugs(_,_),fill(2)).
move(jugs(V1,_),empty(1)):-V1>0.
move(jugs(_,V2),empty(2)):-V2>0.
move(jugs(_,V2),transfer(2,1)):-V2>0.
move(jugs(V1,_),transfer(1,2)):-V1>0.

update(jugs(_,V2),empty(1),jugs(0,V2)).
update(jugs(V1,_),empty(2),jugs(V1,0)).
update(jugs(_,V2),fill(1),jugs(C1,V2)):-capacity(1,C1).
update(jugs(V1,_),fill(2),jugs(V1,C2)):-capacity(2,C2).
update(jugs(V1,V2),transfer(2,1),jugs(W1,W2)):-
    capacity(1,C1),
    Liquid is V1+V2,
    Excess is Liquid-C1,
    (Excess=<0->W1=Liquid,W2=0;
     W1 is C1,W2=Excess).
update(jugs(V1,V2),transfer(1,2),jugs(W1,W2)):-
    capacity(2,C2),
    Liquid is V1+V2,
    Excess is Liquid-C2,
    (Excess=<0->W2=Liquid,W1=0;
     W2 is C2,W1=Excess).
