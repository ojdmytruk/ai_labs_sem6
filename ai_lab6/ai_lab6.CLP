(deftemplate state	
	(slot 3jar)
	(slot 5can)
	(slot parent)
	(slot level)
	(slot node)
	(slot s-action)
)

(deffacts InitialState
	(state (3jar 0)(5can 0)(level 1)(s-action "Start: jars and can are empty"))
)


(defrule state-initial
	(initial-fact)
	=>
	(assert (node 1))
)


(defrule fill-jar
	?e <- (state(3jar ?capacityJar)(5can ?capacityCan)(level ?level))
	?n <- (node ?node)
	(not(state (3jar 3)(5can ?capacityCan)))
	(not(state (3jar 0)(5can 4)))
	(test(and(numberp ?capacityJar)(< ?capacityJar 3)))
	=>
	(assert (state (3jar 3)(5can ?capacityCan)(level (+ ?node 1))(parent ?e)(s-action "Fill jar completely")))
	(retract ?n)
	(assert (node (+ ?node 1)))
	(printout t "Jar: fully filled" crlf)
)


(defrule fill-can
	?e <- (state(3jar ?capacityJar)(5can ?capacityCan)(level ?level))
	?n <- (node ?node)
	(not(state (3jar ?capacityJar)(5can 5)))
	(not(state (3jar 0)(5can 4)))
	(test(and(numberp ?capacityCan)(< ?capacityCan 5)))
	=>
	(assert (state (3jar ?capacityJar)(5can 5)(level (+ ?node 1))(parent ?e)(s-action "Fill can completely")))
	(retract ?n)
	(assert (node (+ ?node 1)))
	(printout t "Can: fully filled" crlf)
)


(defrule fill-jar-with-water-from-can
	?e <- (state (3jar ?capacityJar)(5can ?capacityCan)(level ?level))
	?n <- (node ?node)
	(not (state (3jar 3) (5can =(- ?capacityCan (- 3 ?capacityJar)))))
	(not (state (3jar 0)(5can 4)))
	(test(and(< ?capacityJar 3)(> (+ ?capacityCan ?capacityJar) 3)(> ?capacityJar 0)))
	=>
	(assert (state (3jar 3)(5can =(- ?capacityCan (- 3 ?capacityJar))) (level (+ ?node 1))(parent ?e)(s-action "Fill jar 3 with part of water from can 5")))
	(retract ?n)
	(assert (node (+ ?node 1)))
	(printout t "Jar: filled with part of water from can 5" crlf)
)


(defrule fill-can-with-water-from-jar
	?e <- (state (3jar ?capacityJar)(5can ?capacityCan)(level ?level))
	?n <- (node ?node)
	(not (state (3jar =(- ?capacityJar (- 5 ?capacityCan))) (5can 5)))
	(not (state (3jar 0)(5can 4)))
	(test(and(> ?capacityJar 0)(> (+ ?capacityCan ?capacityJar) 5)(< ?capacityCan 5)))
	=>
	(assert (state (3jar =(- ?capacityJar (- 5 ?capacityCan))) (5can 5) (level (+ ?node 1))(parent ?e)(s-action "Fill can with part of water from jar")))
	(retract ?n)
	(assert (node (+ ?node 1)))
	(printout t "Can: filled with part of water from jar" crlf)
)


(defrule empty-jar
	?e <- (state (3jar ?capacityJar)(5can ?capacityCan)(level ?level))
	?n <- (node ?node)
	(not (state (3jar 0)(5can ?capacityCan)))
	(not (state (3jar 0)(5can 4)))
	(test(> ?litrosj3 0))
	=>
	(assert (state (3jar 0)(5can ?capacityCan)(level (+ ?node 1))(parent ?e)(s-action "Jar completely emptied")))
	(retract ?n)
	(assert (node (+ ?node 1)))
	(printout t "Jar: emptied" crlf)

)


(defrule empty-can
	?e <- (state (3jar ?capacityJar)(5can ?capacityCan)(level ?level))
	?n <- (node ?node)
	(not (state (5can 0)(3jar ?capacityJar)))
	(not (state (3jar 0)(5can 4)))
	(test(> ?litrosj5 0))
	=>
	(assert (state (3jar ?capacityJar)(5can 0)(level (+ ?node 1))(parent ?e)(s-action "Can completely emptied")))
	(retract ?n)
	(assert (node (+ ?node 1)))
	(printout t "Can: emptied" crlf)
)


(defrule empty-from-jar-to-can
	?e <- (state (3jar ?capacityJar)(5can ?capacityCan)(level ?level))
	?n <- (node ?node)
	(not (state (3jar 0)(5can =(+ ?capacityCan ?capacityJar))))
	(not (state (3jar 0)(5can 4)))
	(test(and(> ?capacityCan 0)(<= (+ ?capacityCan ?capacityJar) 5)))
	=>
	(assert (state (3jar 0)(5can =(+ ?capacityCan ?capacityJar)) (level (+ ?node 1))(parent ?e)(s-action "Empty jar into can")))
	(retract ?n)
	(assert (node (+ ?node 1)))
	(printout t "Jar emptied into can" crlf)
)


(defrule empty-from-can-to-jar
	?e <- (state (3jar ?capacityJar)(5can ?capacityCan)(level ?level))
	?n <- (node ?node)
	(not (state (5can 0)(3jar 3)))
	(not (state (3jar 0)(5can 4)))
	(test(and(> ?capacityCan 0)(<= (+ ?capacityCan ?capacityJar) 3)))
	=>
	(assert (state (3jar =(+ ?capacityCan ?capacityJar)) (5can 0) (level (+ ?node 1))(parent ?e)(s-action "Empty can into jar")))
	(retract ?n)
	(assert (node (+ ?node 1)))
	(printout t "Can emptied into jar" crlf)
)


(defrule goal-achieved
	?e <- (state(3jar 0)(5can 4))
	=>
	(printout t "Goal state is achieved" crlf)
	(assert(obtain-parent ?e))
	(assert (path))
)


(defrule result-path
	?e <- (state (parent ?parent) (s-action ?action))
	?r <- (obtainparent ?e)
	?c <- (path $?pathactual)
	=>
	(assert (path ?action ?pathactual))
	(assert (obtainparent ?parent))
	(retract ?c)
	(retract ?r)
)


(defrule finished
?rec <- (obtainparent nil)
?list <- (path $?pathcompleted)
=>
(printout t "Solucion:" ?pathcompleted crlf)
(retract ?rec ?list)
)