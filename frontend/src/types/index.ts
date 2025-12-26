export interface LoginRequest {
  username: string;
  password: string;
}

export interface LoginResponse {
  token: string;
  username: string;
  fullName: string;
  role: string;
  expiresAt: string;
}

export interface User {
  token: string;
  username: string;
  fullName: string;
  role: string;
}

export interface AddressInfo {
  addressType: string;
  street: string;
  city: string;
  state: string;
  postalCode: string;
  country: string;
  isDefault: boolean;
}

export interface CustomerInfo {
  id: number;
  fullName: string;
  email: string;
  phone: string;
  customerType: string;
  dateJoined: string;
  preferredContactMethod: string;
  notes: string;
  addresses: AddressInfo[];
}

export interface OrderInfo {
  id: number;
  orderNumber: string;
  orderDate: string;
  estimatedDeliveryDate: string | null;
  actualDeliveryDate: string | null;
  currentStatus: string;
  subtotal: number;
  taxAmount: number;
  shippingCost: number;
  discountAmount: number;
  totalAmount: number;
  paymentMethod: string;
  orderSource: string;
  specialInstructions: string;
  isGift: boolean;
  giftMessage: string;
  itemCount: number;
}

export interface OrderItemInfo {
  id: number;
  sku: string;
  productName: string;
  productDescription: string;
  category: string;
  quantity: number;
  unitPrice: number;
  discountPercent: number;
  lineTotal: number;
  status: string;
  returnReason: string | null;
}

export interface StatusHistoryInfo {
  id: number;
  status: string;
  timestamp: string;
  updatedBy: string;
  notes: string;
  location: string;
}

export interface ShippingInfo {
  carrier: string;
  serviceType: string;
  trackingNumber: string;
  trackingUrl: string;
  shippingWeight: number;
  packageCount: string;
  shippingAddress: AddressInfo;
  shippedDate: string | null;
  deliveredDate: string | null;
  deliverySignature: string;
  deliveryInstructions: string;
}

export interface PaymentDetailInfo {
  paymentMethod: string;
  paymentStatus: string;
  transactionId: string;
  cardLastFour: string;
  cardBrand: string;
  paymentDate: string;
  amountPaid: number;
  refundedAmount: number | null;
  billingAddress: AddressInfo;
}

export interface ServiceNoteInfo {
  id: number;
  agentName: string;
  noteType: string;
  content: string;
  createdAt: string;
  isInternal: boolean;
  priority: string;
  requiresFollowup: boolean;
  followupDate: string | null;
}

export interface OrderDetailResponse {
  order: OrderInfo;
  customer: CustomerInfo;
  items: OrderItemInfo[];
  statusHistory: StatusHistoryInfo[];
  shipping: ShippingInfo | null;
  payment: PaymentDetailInfo | null;
  serviceNotes: ServiceNoteInfo[];
}
