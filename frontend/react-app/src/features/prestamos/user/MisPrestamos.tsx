import { useEffect } from "react";
import { useAppDispatch, useAppSelector } from "../../../app/hooks";
import { PrestamosTable } from "../components/";
import { getPrestamosAll } from "../api";

export default function MisPrestamos() {
  const dispatch = useAppDispatch();
   const { items } = useAppSelector((s) => s.prestamos);
  const { email } = useAppSelector((s) => s.auth);
  
useEffect(() => {
    dispatch(getPrestamosAll());
  }, [dispatch]);
  
  const misPrestamos = items.filter((p) => p.usuario === email);
  return <PrestamosTable title="Mis préstamos" data={misPrestamos} />;
}