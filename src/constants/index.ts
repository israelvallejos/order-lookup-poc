export const formatCurrency = (amount: number) => {
  return new Intl.NumberFormat('es-CL', {
    style: 'currency',
    currency: 'CLP',
    minimumFractionDigits: 0,
  }).format(amount);
};

export const formatDate = (dateString: string | null) => {
  if (!dateString) return '-';
  return new Date(dateString).toLocaleString('es-CL', {
    dateStyle: 'medium',
    timeStyle: 'short',
  });
};

export const getStatusColor = (status: string) => {
  const statusLower = status.toLowerCase();
  if (statusLower.includes('delivered') || statusLower.includes('completed') || statusLower.includes('entregado')) return 'success';
  if (statusLower.includes('issue') || statusLower.includes('return') || statusLower.includes('failed') || statusLower.includes('problema')) return 'error';
  if (statusLower.includes('processing') || statusLower.includes('picking') || statusLower.includes('transit') || statusLower.includes('proceso')) return 'info';
  if (statusLower.includes('pending') || statusLower.includes('awaiting') || statusLower.includes('pendiente')) return 'warning';
  return 'default';
};

export const getPriorityColor = (priority: string) => {
  switch (priority.toLowerCase()) {
    case 'urgent': case 'urgente': return 'error';
    case 'high': case 'alta': return 'warning';
    case 'normal': return 'info';
    case 'low': case 'baja': return 'default';
    default: return 'default';
  }
};

export const translateStatus = (status: string) => {
  const translations: Record<string, string> = {
    'Pending': 'Pendiente',
    'Payment Confirmed': 'Pago Confirmado',
    'Processing': 'En Proceso',
    'Picking': 'En Picking',
    'Quality Check': 'Control de Calidad',
    'Packed': 'Empacado',
    'Ready for Pickup': 'Listo para Despacho',
    'Shipped': 'Despachado',
    'In Transit': 'En Tránsito',
    'Out for Delivery': 'En Reparto',
    'Delivery Attempted': 'Intento de Entrega',
    'Delivered': 'Entregado',
    'Issue Reported': 'Problema Reportado',
    'Issue - Return Requested': 'Problema - Devolución Solicitada',
    'Awaiting Payment': 'Esperando Pago',
    'Gift Wrapping': 'Envoltorio de Regalo',
    'Return Requested': 'Devolución Solicitada',
    'Picked': 'Recogido',
    'Returned': 'Devuelto',
    'Captured': 'Capturado',
    'Authorized': 'Autorizado',
    'Refunded': 'Reembolsado',
    'Failed': 'Fallido',
  };
  return translations[status] || status;
};

export const translateNoteType = (type: string) => {
  const translations: Record<string, string> = {
    'General': 'General',
    'Complaint': 'Reclamo',
    'Inquiry': 'Consulta',
    'Resolution': 'Resolución',
    'Followup': 'Seguimiento',
  };
  return translations[type] || type;
};

export const translatePriority = (priority: string) => {
  const translations: Record<string, string> = {
    'Low': 'Baja',
    'Normal': 'Normal',
    'High': 'Alta',
    'Urgent': 'Urgente',
  };
  return translations[priority] || priority;
};

export const translateCustomerType = (type: string) => {
  const translations: Record<string, string> = {
    'Regular': 'Regular',
    'VIP': 'VIP',
    'Wholesale': 'Mayorista',
  };
  return translations[type] || type;
};

export const translateAddressType = (type: string) => {
  const translations: Record<string, string> = {
    'Shipping': 'Envío',
    'Billing': 'Facturación',
  };
  return translations[type] || type;
};

