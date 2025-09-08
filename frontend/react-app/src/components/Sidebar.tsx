import { NavLink } from "react-router-dom";
import { useAppSelector } from "../app/hooks";

const linkClass =
  "block px-4 py-2 rounded-lg hover:bg-indigo-50 hover:text-indigo-700 transition text-sm font-medium";

export default function Sidebar() {
  const { rol } = useAppSelector((s) => s.auth);
  const isAdmin = rol?.toLowerCase() === "admin";

  return (
    <aside className="w-60 bg-white border-r min-h-screen p-4">
      <h2 className="text-lg font-bold text-indigo-600 mb-6">Menú</h2>
      <nav className="space-y-2">
       
            {/* Links comunes (usuario) */}
        <NavLink
          to="/dashboard"
          className={({ isActive }) =>
            `${linkClass} ${isActive ? "bg-indigo-100 text-indigo-700" : "text-gray-700"}`
          }
        >
          Dashboard
        </NavLink>
         
         <NavLink
          to="/prestamos/solicitar"
          className={({ isActive }) =>
            `${linkClass} ${isActive ? "bg-indigo-100 text-indigo-700" : "text-gray-700"}`
          }
        >
          Solicitar Prestamo
        </NavLink>

   
        <NavLink
          to="/prestamos/user"
          className={({ isActive }) =>
            `${linkClass} ${isActive ? "bg-indigo-100 text-indigo-700" : "text-gray-700"}`
          }
        >
          Mis Préstamos
        </NavLink>

        {/* Links solo admin */}
        {isAdmin && (
          <>
            <NavLink
              to="/admin/pendientes"
              className={({ isActive }) =>
                `${linkClass} ${isActive ? "bg-indigo-100 text-indigo-700" : "text-gray-700"}`
              }
            >
              Pendientes
            </NavLink>
          
          </>
        )}
      </nav>
    </aside>
  );
}