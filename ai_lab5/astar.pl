solve_astar(Node, Path/Cost):-
  estimate(Node, Estimate),
  astar([[Node]/0/Estimate], RevPath/Cost/_),
  reverse(RevPath, Path).

move_astar([Node|Path]/Cost/_, [NextNode,Node|Path]/NewCost/Est):-
  move(Node, NextNode, StepCost),
  \+ member(NextNode, Path),
  NewCost is Cost + StepCost,
  estimate(NextNode, Est).


expand_astar(Path, ExpPaths):-
  findall(NewPath, move_astar(Path,NewPath), ExpPaths).

get_best([Path], Path):- !.

get_best([Path1/Cost1/Est1,_/Cost2/Est2|Paths], BestPath):-
  Cost1 + Est1 =< Cost2 + Est2, !,
  get_best([Path1/Cost1/Est1|Paths], BestPath).

get_best([_|Paths], BestPath):-
  get_best(Paths, BestPath).

astar(Paths, Path):-
  get_best(Paths, Path),
  Path = [Node|_]/_/_,
  goal(Node).
astar(Paths, SolutionPath) :-
  get_best(Paths, BestPath),
  select(BestPath, Paths, OtherPaths),
  expand_astar(BestPath, ExpPaths),
  append(OtherPaths, ExpPaths, NewPaths),
  astar(NewPaths, SolutionPath).

estimate(Node, Estimate):-
  goal(G),
  count_diffs(Node, G, Estimate).

count_diffs(X,X,0):- !.
count_diffs([H|T1],[H|T2],Result):-
  count_diffs(T1,T2,Result).
count_diffs([H1|T1],[H2|T2],Result):-
  H1 \== H2,
  count_diffs(T1,T2,Result1),
  Result is Result1+1.

goal([0,0,0]).

move([МісіонериДо,КанібалиДо,1], [МісіонериПісля,КанібалиПісля,0], 1) :-
    МісіонериПісля is МісіонериДо-1, КанібалиПісля is КанібалиДо-1,
    not(illegal(state(МісіонериПісля,КанібалиПісля,0))).

move([МісіонериДо,КанібалиДо,1], [МісіонериПісля,КанібалиПісля,0], 1) :-
    МісіонериПісля is МісіонериДо-2, КанібалиПісля is КанібалиДо,
    not(illegal(state(МісіонериПісля,КанібалиПісля,0))).

move([МісіонериДо,КанібалиДо,1], [МісіонериПісля,КанібалиПісля,0], 1) :-
    МісіонериПісля is МісіонериДо, КанібалиПісля is КанібалиДо-2,
    not(illegal(state(МісіонериПісля,КанібалиПісля,0))).

move([МісіонериДо,КанібалиДо,1], [МісіонериПісля,КанібалиПісля,0], 1) :-
    МісіонериПісля is МісіонериДо-1, КанібалиПісля is КанібалиДо,
    not(illegal(state(МісіонериПісля,КанібалиПісля,0))).

move([МісіонериДо,КанібалиДо,1], [МісіонериПісля,КанібалиПісля,0], 1) :-
    МісіонериПісля is МісіонериДо, КанібалиПісля is КанібалиДо-1,
    not(illegal(state(МісіонериПісля,КанібалиПісля,0))).

move([МісіонериДо,КанібалиДо,0], [МісіонериПісля,КанібалиПісля,1], 1) :-
    МісіонериПісля is МісіонериДо+1, КанібалиПісля is КанібалиДо+1,
    not(illegal(state(МісіонериПісля,КанібалиПісля,1))).

move([МісіонериДо,КанібалиДо,0], [МісіонериПісля,КанібалиПісля,1], 1) :-
    МісіонериПісля is МісіонериДо+2, КанібалиПісля is КанібалиДо,
    not(illegal(state(МісіонериПісля,КанібалиПісля,1))).

move([МісіонериДо,КанібалиДо,0], [МісіонериПісля,КанібалиПісля,1], 1) :-
    МісіонериПісля is МісіонериДо, КанібалиПісля is КанібалиДо+2,
    not(illegal(state(МісіонериПісля,КанібалиПісля,1))).

move([МісіонериДо,КанібалиДо,0], [МісіонериПісля,КанібалиПісля,1], 1) :-
    МісіонериПісля is МісіонериДо+1, КанібалиПісля is КанібалиДо,
    not(illegal(state(МісіонериПісля,КанібалиПісля,1))).

move([МісіонериДо,КанібалиДо,0], [МісіонериПісля,КанібалиПісля,1], 1) :-
    МісіонериПісля is МісіонериДо, КанібалиПісля is КанібалиДо+1,
    not(illegal(state(МісіонериПісля,КанібалиПісля,1))).

illegal(state(M,_,_)) :- M < 0.
illegal(state(M,_,_)) :- M > 3.
illegal(state(_,C,_)) :- C < 0.
illegal(state(_,C,_)) :- C > 3.

illegal(state(1,3,0)).
illegal(state(2,3,0)).
illegal(state(1,2,_)).

illegal(state(3,1,0)).
illegal(state(3,2,0)).
illegal(state(2,1,_)).

illegal(state(2,0,1)).
illegal(state(0,2,1)).
illegal(state(1,0,_)).
illegal(state(3,3,1)).
