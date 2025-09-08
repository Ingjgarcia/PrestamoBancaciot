import { useEffect } from "react";
import { useAppDispatch, useAppSelector } from "../../../app/hooks";
import { getPrestamosPendientes } from "../api";
import { PrestamosTable } from "../components";

export default function Pendientes() {
  const dispatch = useAppDispatch();
  const { pendientes } = useAppSelector((s) => s.prestamos);

  useEffect(() => {
    dispatch(getPrestamosPendientes());
  }, [dispatch]);

  return <PrestamosTable title="Préstamos pendientes" data={pendientes} showActions />;
}