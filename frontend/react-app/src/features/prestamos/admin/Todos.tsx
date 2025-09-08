import { useAppSelector } from "../../../app/hooks";
import { PrestamosTable } from "../components";

export default function Todos() {
  const { items } = useAppSelector((s) => s.prestamos);
  return <PrestamosTable title="Todos los préstamos" data={items} />;
}