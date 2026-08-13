function Card({ children, className = '' }) {
  return (
    <div
      className={`bg-white rounded-2xl border border-charcoal/10 shadow-[0_1px_3px_rgba(0,0,0,0.06)] p-5 ${className}`}
    >
      {children}
    </div>
  )
}

export default Card
