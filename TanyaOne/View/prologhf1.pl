app2([HD|TL],N-M,L) :- N=0, L=HD.
app2([HD|TL],N-M,L) :- N1 is N-1, app2(TL, N1-M, L1), L=[HD|L1].
app1([HD|[TLHD|TL]],N-M,L) :- N1 is N-1, app2(HD, N1-M, L1),app2(TL, N1-M, L2), L=[L1|L2].


app1([[a,b,c,d],[e,f,g,h]],2-2,L).