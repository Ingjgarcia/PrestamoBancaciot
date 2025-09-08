import { useAppDispatch } from "../../../app/hooks";
import { formatDate } from "../../../app/utils/formatDate";
import Badge from "../../../components/Badge";
import { Card } from "../../../components/Card";
import { EstadoPrestamo } from "../../../types/Prestamos/EstadoPrestamo";
import type { Prestamo } from "../../../types/Prestamos/Prestamo";
import { aprobarPrestamo, getPrestamosAll, getPrestamosPendientes, rechazarPrestamo } from "../api";

export function PrestamosTable({ title, data, showActions }: { title: string; data: Prestamo[]; showActions?: boolean }) {
    const dispatch = useAppDispatch();
    return (
        <Card title={title}>
            <div className="overflow-auto">
                <table className="min-w-full text-sm">
                    <thead>
                        <tr className="text-left border-b">
                            <th className="py-2 pr-4"></th>
                            <th className="py-2 pr-4">Usuario</th>
                            <th className="py-2 pr-4">Cantidad</th>
                            <th className="py-2 pr-4">Tiempo</th>
                            <th className="px-4 py-2">F. Solicitud</th>
                            <th className="px-4 py-2">F. actualizacion</th>
                            <th className="py-2 pr-4">Estado</th>
                            {showActions && <th className="py-2 pr-4">Acciones</th>}
                        </tr>
                    </thead>
                    <tbody>
                        {data.map((p, index) => (
                            <tr key={p.id} className="border-b last:border-none">
                                <td className="py-2 pr-4 font-mono text-xs">{index + 1}</td>
                                <td className="py-2 pr-4">{p.usuario}</td>
                                <td className="py-2 pr-4">${p.cantidad.toLocaleString()}</td>
                                <td className="py-2 pr-4">{p.tiempo}</td>
                                <td className="py-2 pr-4">{formatDate(p.fechaCreacion)}</td>
                                <td className="py-2 pr-4">{formatDate(p.fechaModificacion)}</td>
                                <td className="py-2 pr-4">
                                    {p.estado === EstadoPrestamo.Aprobado && <Badge color="green">Aprobado</Badge>}
                                    {p.estado === EstadoPrestamo.Rechazado && <Badge color="red">Rechazado</Badge>}
                                    {(!p.estado || p.estado === EstadoPrestamo.pendiente) && <Badge color="yellow">Pendiente</Badge>}
                                </td>
                                {showActions && (
                                    <td className="py-2 pr-4">
                                        <div className="flex gap-2">
                                            <button
                                                onClick={() => dispatch(aprobarPrestamo(p.id))
                                                    .then(() => dispatch(getPrestamosPendientes()))
                                                    .then(() => dispatch(getPrestamosAll()))
                                                }
                                                className="rounded-lg bg-green-600 text-white px-3 py-1 text-xs hover:bg-green-700"
                                            >
                                                Aprobar
                                            </button>
                                            <button
                                                onClick={() => dispatch(rechazarPrestamo(p.id))
                                                    .then(() => dispatch(getPrestamosPendientes())
                                                    .then(() => dispatch(getPrestamosAll())))}
                                                className="rounded-lg bg-red-600 text-white px-3 py-1 text-xs hover:bg-red-700"
                                            >
                                                Rechazar
                                            </button>
                                        </div>
                                    </td>
                                )}
                            </tr>
                        ))}
                    </tbody>
                </table>
            </div>
        </Card>
    );
}