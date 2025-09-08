import { BrowserRouter, Routes, Route } from "react-router-dom";
import { Provider } from "react-redux";
import { store } from "./app/store";
import LoginPage from "./pages/LoginPage";
import Dashboard from "./pages/Dashboard";
import NotFound from "./pages/NotFound";
import PrivateRoute from "./routes/PrivateRoute";
import AdminRoute from "./routes/AdminRoute";
import Pendientes from "./features/prestamos/admin/Pendientes";
import Todos from "./features/prestamos/admin/Todos";
import Layout from "./components/Layout";
import SolicitarPrestamo from "./features/prestamos/user/SolicitarPrestamo";
import MisPrestamos from "./features/prestamos/user/MisPrestamos";

export default function App() {
  return (
    <Provider store={store}>
      <BrowserRouter>
        <Routes>
          {/* Public */}
          <Route path="/login" element={<LoginPage />} />

          {/* Usuario */}
          <Route
            path="/dashboard"
            element={
              <PrivateRoute>
                <Layout>
                  <Dashboard />
                </Layout>
              </PrivateRoute>
            }
          />
          <Route
            path="/prestamos/solicitar"
            element={
              <PrivateRoute>
                <Layout>
                  <SolicitarPrestamo />
                </Layout>
              </PrivateRoute>
            }
          />
           <Route
            path="/prestamos/user"
            element={
              <PrivateRoute>
                <Layout>
                  <MisPrestamos />
                </Layout>
              </PrivateRoute>
            }
          />
          {/* Admin */}
          <Route
            path="/admin/pendientes"
            element={
              <AdminRoute>
                <Layout>
                  <Pendientes />
                </Layout>
              </AdminRoute>
            }
          />
          <Route
            path="/admin/todos"
            element={
              <AdminRoute>
                <Layout>
                  <Todos />
                </Layout>
              </AdminRoute>
            }
          />

          {/* Not Found */}
          <Route path="*" element={<NotFound />} />
        </Routes>
      </BrowserRouter>
    </Provider>
  );
}