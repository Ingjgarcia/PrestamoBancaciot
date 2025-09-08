import { useState } from "react";
import { useAppDispatch, useAppSelector } from "../../../app/hooks";
import { useNavigate } from "react-router-dom";
import { addPrestamo, getPrestamosAll, getPrestamosPendientes } from "../api";

export default function CreatePrestamoCard() {
    const dispatch = useAppDispatch();
    const navigate = useNavigate();
    const { userId } = useAppSelector((s) => s.auth);

    const [cantidad, setCantidad] = useState("");
    const [tiempo, setTiempo] = useState("");
    const [showModal, setShowModal] = useState(false);

    const handleSubmit = (e: React.FormEvent) => {
        e.preventDefault();

        if (!userId || !cantidad || !tiempo) return;

        dispatch(addPrestamo({
            idUsuario: userId,
            cantidad: parseFloat(cantidad),
            tiempo: parseInt(tiempo),
        })).then(() => {
            dispatch(getPrestamosAll()).then(() => {
                dispatch(getPrestamosPendientes());

            });
        });

        // ✅ Limpiar formulario
        setCantidad("");
        setTiempo("");

        // ✅ Mostrar modal de confirmación
        setShowModal(true);
    };

    return (
        <div className="p-6 bg-white rounded-2xl shadow-md">
            <h2 className="text-xl font-semibold text-gray-900 mb-4">Nuevo Préstamo</h2>

            <form onSubmit={handleSubmit} className="space-y-4">
                {/* Cantidad decimal */}
                <div>
                    <label className="block text-sm font-medium text-gray-700">Cantidad</label>
                    <input
                        type="number"
                        step="0.01"        // ✅ Permite decimales
                        value={cantidad}
                        onChange={(e) => setCantidad(e.target.value)}
                        className="mt-1 w-full rounded-lg border px-3 py-2 bg-gray-100 focus:bg-white focus:ring-2 focus:ring-indigo-300"
                        placeholder="0.00"
                    />
                </div>

                {/* Tiempo en meses */}
                <div>
                    <label className="block text-sm font-medium text-gray-700">Tiempo (meses)</label>
                    <input
                        type="number"
                        value={tiempo}
                        onChange={(e) => setTiempo(e.target.value)}
                        className="mt-1 w-full rounded-lg border px-3 py-2 bg-gray-100 focus:bg-white focus:ring-2 focus:ring-indigo-300"
                        placeholder="0"
                    />
                </div>

                <button
                    type="submit"
                    className="w-full py-2 rounded-lg bg-indigo-600 text-white font-semibold hover:bg-indigo-700"
                >
                    Crear Préstamo
                </button>
            </form>

            {/* ✅ Modal de confirmación */}
            {showModal && (
                <div className="fixed inset-0 flex items-center justify-center bg-black bg-opacity-40">
                    <div className="bg-white rounded-2xl p-6 shadow-lg max-w-sm w-full">
                        <h3 className="text-lg font-bold text-gray-900 mb-2">
                            ✅ Préstamo creado
                        </h3>
                        <p className="text-sm text-gray-600 mb-4">
                            El préstamo se ha registrado correctamente.
                        </p>
                        <div className="flex justify-end gap-3">
                            <button
                                onClick={() => setShowModal(false)}
                                className="px-4 py-2 rounded-lg bg-gray-200 hover:bg-gray-300 text-gray-800"
                            >
                                Cerrar
                            </button>
                            <button
                                onClick={() => navigate("/prestamos/user")}
                                className="px-4 py-2 rounded-lg bg-indigo-600 text-white hover:bg-indigo-700"
                            >
                                Mis Préstamos
                            </button>
                        </div>
                    </div>
                </div>
            )}
        </div>
    );
}
