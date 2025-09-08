
import React, { useState } from 'react';

function Login({onAuth}) {
  const [email,setEmail] = useState('');
  const [password,setPassword] = useState('');
  const submit = async e => {
    e.preventDefault();
    const res = await fetch((import.meta.env.VITE_API_URL||'http://localhost:5000')+'/api/auth/login',{method:'POST',headers:{'content-type':'application/json'},body:JSON.stringify({email,password})});
    if (!res.ok) { alert('Login failed'); return; }
    const j = await res.json();
    localStorage.setItem('token', j.token);
    localStorage.setItem('email', j.email);
    localStorage.setItem('role', j.role);
    onAuth({token:j.token,email:j.email,role:j.role});
  };
  return <form onSubmit={submit}>
    <h3>Login</h3>
    <input placeholder="email" value={email} onChange={e=>setEmail(e.target.value)} />
    <input placeholder="password" type="password" value={password} onChange={e=>setPassword(e.target.value)} />
    <button>Login</button>
  </form>;
}

function RequestLoan(){ 
  const [amount,setAmount]=useState(1000);
  const [term,setTerm]=useState(12);
  const submit = async e=>{
    e.preventDefault();
    const token = localStorage.getItem('token');
    const res = await fetch((import.meta.env.VITE_API_URL||'http://localhost:5000')+'/api/loans',{method:'POST',headers:{'content-type':'application/json','authorization':'Bearer '+token},body:JSON.stringify({amount,termMonths:term})});
    if (res.ok) alert('Request sent');
    else alert('Failed');
  };
  return <form onSubmit={submit}>
    <h3>Request Loan</h3>
    <input type="number" value={amount} onChange={e=>setAmount(parseFloat(e.target.value))} />
    <input type="number" value={term} onChange={e=>setTerm(parseInt(e.target.value))} />
    <button>Send</button>
  </form>;
}

function MyLoans(){
  const [loans,setLoans]=useState([]);
  React.useEffect(()=>{ const t=localStorage.getItem('token'); fetch((import.meta.env.VITE_API_URL||'http://localhost:5000')+'/api/loans/my',{headers:{authorization:'Bearer '+t}}).then(r=>r.json()).then(setLoans).catch(()=>{}); },[]);
  return <div><h3>My Loans</h3><pre>{JSON.stringify(loans,null,2)}</pre></div>;
}

export default function App(){
  const [auth,setAuth] = useState(()=>{ const t=localStorage.getItem('token'); if (!t) return null; return {token:t,email:localStorage.getItem('email'),role:localStorage.getItem('role')}; });
  if (!auth) return <Login onAuth={setAuth} />;
  return <div>
    <h2>Welcome, {auth.email}</h2>
    <RequestLoan/>
    <MyLoans/>
  </div>;
}
