export function Card(props: React.PropsWithChildren<{ title?: string; className?: string }>) {
  return (
    <div className={`rounded-2xl shadow p-6 bg-white/80 backdrop-blur border border-gray-100 ${props.className || ""}`}>
      {props.title && <h2 className="text-xl font-semibold mb-4">{props.title}</h2>}
      {props.children}
    </div>
  );
}