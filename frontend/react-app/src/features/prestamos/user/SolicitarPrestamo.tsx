import CreatePrestamoCard from "../components/CreatePrestamoCard";

export default function SolicitarPrestamo() {
  return (
    <div className="max-w-xl mx-auto mt-6">
      <h1 className="text-2xl font-bold text-gray-900 mb-4">Solicitar nuevo préstamo</h1>
      <CreatePrestamoCard />
    </div>
  );
}
