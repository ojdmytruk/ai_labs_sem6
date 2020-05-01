% capacity(номер_посудини, ємність_у_літрах)

% ємність першої посудини (бідона) - 5 л
capacity(1,5).
% ємність другої посудини (банки) - 3 л
capacity(2,3).

% Фінальний стан
% final_state(vessel(заповненість_бідона,заповненість_банки)).
final_state(vessel(4,_)).

% Перехід до нових станів
% Номери посудин: 1 - бідон, 2 - банка
% state_change(vessel(заповненість_бідона,заповненість_банки),дія(номер_посудини)).
state_change(vessel(Volume1,_),наповнення(1)):-Volume1<5.
state_change(vessel(_,Volume2),наповнення(2)):-Volume2<3.
state_change(vessel(Volume1,_),спорожнення(1)):-Volume1>0.
state_change(vessel(_,Volume2),спорожнення(2)):-Volume2>0.

% state_change(vessel(заповненість_бідона,заповненість_банки),переливання(з_якої_посудини,в_яку_посудину)).
state_change(vessel(Volume1,_),переливання(1,2)):-Volume1>0.
state_change(vessel(_,Volume2),переливання(2,1)):-Volume2>0.

% Оновлення заповненості посудин
update(vessel(_,Volume2),спорожнення(1),vessel(0,Volume2)).
update(vessel(Volume1,_),спорожнення(2),vessel(Volume1,0)).
update(vessel(_,Volume2),наповнення(1),vessel(Capacity1,Volume2)):-capacity(1,Capacity1).
update(vessel(Volume1,_),наповнення(2),vessel(Volume1,Capacity2)):-capacity(2,Capacity2).
update(vessel(Volume1,Volume2),переливання(2,1),vessel(New_volume1,New_volume2)):-
    capacity(1,Capacity1),
    Water is Volume1+Volume2,
    Overflow is Water-Capacity1,
    (Overflow=<0->New_volume1=Water,New_volume2=0;
     New_volume1 is Capacity1,New_volume2=Overflow).
update(vessel(Volume1,Volume2),переливання(1,2),vessel(New_volume1,New_volume2)):-
    capacity(2,Capacity2),
    Water is Volume1+Volume2,
    Overflow is Water-Capacity2,
    (Overflow=<0->New_volume2=Water,New_volume1=0;
     New_volume2 is Capacity2,New_volume1=Overflow).

% реалізація пошуку з обмеженням глибини
solve_dls(MaxDepth,State,[]):-
    MaxDepth > 0,
    final_state(State).
solve_dls(MaxDepth,State,[State_change|State_changes]):-
    MaxDepth > 0,
    state_change(State,State_change),
    update(State,State_change,State1),
    MaxDepth1 is MaxDepth - 1,
    solve_dls(MaxDepth1,State1,State_changes).