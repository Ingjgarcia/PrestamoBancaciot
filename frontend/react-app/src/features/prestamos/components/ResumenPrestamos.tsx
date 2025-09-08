import { useAppSelector } from "../../../app/hooks";
import { EstadoPrestamo } from "../../../types/Prestamos/EstadoPrestamo";

export default function ResumenPrestamos() {
  const prestamos = useAppSelector((s) => s.prestamos.items);

  const Aprobados = prestamos.filter(p => p.estado === EstadoPrestamo.Aprobado).length;
  const pendientes = prestamos.filter(p => p.estado === EstadoPrestamo.pendiente).length;
  const rechazados = prestamos.filter(p => p.estado === EstadoPrestamo.Rechazado).length;

  return (
    <div className="grid grid-cols-1 sm:grid-cols-3 gap-4 my-6">
      <div className="p-4 rounded-xl bg-green-100 text-green-800 shadow">
        <h3 className="text-lg font-bold">Aprobados</h3>
        <p className="text-2xl font-semibold">{Aprobados}</p>
      </div>

      <div className="p-4 rounded-xl bg-yellow-100 text-yellow-800 shadow">
        <h3 className="text-lg font-bold">Pendientes</h3>
        <p className="text-2xl font-semibold">{pendientes}</p>
      </div>

      <div className="p-4 rounded-xl bg-red-100 text-red-800 shadow">
        <h3 className="text-lg font-bold">Rechazados</h3>
        <p className="text-2xl font-semibold">{rechazados}</p>
      </div>
    </div>
  );
}