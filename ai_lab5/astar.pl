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

legal(CL,ML,CR,MR) :-
	ML>=0, CL>=0, MR>=0, CR>=0,
	(ML>=CL ; ML=0),
	(MR>=CR ; MR=0),
  (CL>=ML ; CL=0),
	(CR>=MR ; CR=0).


move([CL,ML,left,CR,MR],[CL,ML2,right,CR,MR2],1):-
	MR2 is MR+2,
	ML2 is ML-2,
	legal(CL,ML2,CR,MR2).

move([CL,ML,left,CR,MR],[CL2,ML,right,CR2,MR],1):-
	CR2 is CR+2,
	CL2 is CL-2,
	legal(CL2,ML,CR2,MR).

move([CL,ML,left,CR,MR],[CL2,ML2,right,CR2,MR2],1):-
	CR2 is CR+1,
	CL2 is CL-1,
	MR2 is MR+1,
	ML2 is ML-1,
	legal(CL2,ML2,CR2,MR2).

move([CL,ML,left,CR,MR],[CL,ML2,right,CR,MR2],1):-
	MR2 is MR+1,
	ML2 is ML-1,
	legal(CL,ML2,CR,MR2).

move([CL,ML,left,CR,MR],[CL2,ML,right,CR2,MR],1):-
	CR2 is CR+1,
	CL2 is CL-1,
	legal(CL2,ML,CR2,MR).

move([CL,ML,right,CR,MR],[CL,ML2,left,CR,MR2],1):-
	MR2 is MR-2,
	ML2 is ML+2,
	legal(CL,ML2,CR,MR2).

move([CL,ML,right,CR,MR],[CL2,ML,left,CR2,MR],1):-
	CR2 is CR-2,
	CL2 is CL+2,
	legal(CL2,ML,CR2,MR).

move([CL,ML,right,CR,MR],[CL2,ML2,left,CR2,MR2],1):-
	CR2 is CR-1,
	CL2 is CL+1,
	MR2 is MR-1,
	ML2 is ML+1,
	legal(CL2,ML2,CR2,MR2).

move([CL,ML,right,CR,MR],[CL,ML2,left,CR,MR2],1):-
	MR2 is MR-1,
	ML2 is ML+1,
	legal(CL,ML2,CR,MR2).

move([CL,ML,right,CR,MR],[CL2,ML,left,CR2,MR],1):-
	CR2 is CR-1,
	CL2 is CL+1,
	legal(CL2,ML,CR2,MR).

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

goal([0,0,right,3,3]).
