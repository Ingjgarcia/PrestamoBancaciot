import { useEffect } from "react";
import { getPrestamosAll } from "../features/prestamos/api";
import Todos from "../features/prestamos/admin/Todos";
import { useAppDispatch, useAppSelector } from "../app/hooks";
import ResumenPrestamos from "../features/prestamos/components/ResumenPrestamos";

export default function Dashboard() {
    const dispatch = useAppDispatch();
    const { rol } = useAppSelector((s) => s.auth);

    const isAdmin = rol?.toLowerCase() === "admin";

    useEffect(() => {
        dispatch(getPrestamosAll());
        if (isAdmin) {
            // Pendientes se cargan también desde el componente <Pendientes />
            dispatch(getPrestamosAll());
        }
    }, [dispatch, isAdmin]);

    return (
        <div className="min-h-screen bg-gradient-to-br from-slate-50 to-white">
            <main className="max-w-6xl mx-auto p-4 grid gap-4">

                {/* Bloque superior */}
                 <div>
      <h1 className="text-2xl font-bold text-gray-900 mb-4">Dashboard</h1>
      <ResumenPrestamos />
      {/* Aquí puedes seguir mostrando la tabla de préstamos */}
    </div>

                {/* Sección de admin */}
                {isAdmin && (
                    <>
                      
                        <Todos />
                    </>
                )}
            </main>
        </div>
    );
}
