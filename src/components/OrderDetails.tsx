'use client';

import {
  Box,
  Card,
  CardContent,
  Typography,
  Chip,
  Grid,
  Divider,
  Table,
  TableBody,
  TableCell,
  TableContainer,
  TableHead,
  TableRow,
  Stepper,
  Step,
  StepLabel,
  StepContent,
  Link,
  Accordion,
  AccordionSummary,
  AccordionDetails,
} from '@mui/material';
import {
  Person,
  LocalShipping,
  Payment,
  Inventory,
  History,
  Notes,
  ExpandMore,
  Phone,
  Email,
  LocationOn,
  CardGiftcard,
  Warning,
  CheckCircle,
  Error,
  Schedule,
  Star,
} from '@mui/icons-material';
import { OrderDetailsProps } from '@/interfaces';
import { formatCurrency, formatDate, getStatusColor, getPriorityColor, translateStatus, translateNoteType, translatePriority, translateCustomerType, translateAddressType } from '@/constants';


const getCustomerTypeIcon = (type: string) => {
  switch (type) {
    case 'VIP': return <Star sx={{ color: 'warning.main', fontSize: 18 }} />;
    case 'Wholesale': return <Inventory sx={{ color: 'info.main', fontSize: 18 }} />;
    default: return null;
  }
};

export default function OrderDetails({ data }: OrderDetailsProps) {
  const { order, customer, items, statusHistory, shipping, payment, serviceNotes } = data;

  return (
    <Box sx={{ display: 'flex', flexDirection: 'column', gap: 3 }}>
      <Card>
        <CardContent>
          <Box sx={{ display: 'flex', justifyContent: 'space-between', alignItems: 'flex-start', flexWrap: 'wrap', gap: 2 }}>
            <Box>
              <Typography variant="h5" fontWeight={600}>
                Pedido {order.orderNumber}
              </Typography>
              <Typography variant="body2" color="text.secondary" sx={{ mt: 0.5 }}>
                Realizado el {formatDate(order.orderDate)} vía {order.orderSource}
              </Typography>
            </Box>
            <Box sx={{ display: 'flex', gap: 1, alignItems: 'center' }}>
              {order.isGift && (
                <Chip
                  icon={<CardGiftcard />}
                  label="Pedido Regalo"
                  color="secondary"
                  size="small"
                />
              )}
              <Chip
                label={translateStatus(order.currentStatus)}
                color={getStatusColor(order.currentStatus)}
                sx={{ fontWeight: 600 }}
              />
            </Box>
          </Box>

          <Divider sx={{ my: 2 }} />

          <Grid container spacing={3}>
            <Grid item xs={12} md={3}>
              <Typography variant="caption" color="text.secondary">Subtotal</Typography>
              <Typography variant="body1">{formatCurrency(order.subtotal)}</Typography>
            </Grid>
            <Grid item xs={12} md={3}>
              <Typography variant="caption" color="text.secondary">Impuestos</Typography>
              <Typography variant="body1">{formatCurrency(order.taxAmount)}</Typography>
            </Grid>
            <Grid item xs={12} md={3}>
              <Typography variant="caption" color="text.secondary">Envío</Typography>
              <Typography variant="body1">{order.shippingCost === 0 ? 'GRATIS' : formatCurrency(order.shippingCost)}</Typography>
            </Grid>
            <Grid item xs={12} md={3}>
              <Typography variant="caption" color="text.secondary">Descuento</Typography>
              <Typography variant="body1" color="success.main">-{formatCurrency(order.discountAmount)}</Typography>
            </Grid>
          </Grid>

          <Box sx={{ mt: 2, p: 2, bgcolor: 'primary.main', borderRadius: 1, display: 'flex', justifyContent: 'space-between' }}>
            <Typography variant="h6" color="white">Total</Typography>
            <Typography variant="h6" color="white" fontWeight={600}>{formatCurrency(order.totalAmount)}</Typography>
          </Box>

          {order.specialInstructions && (
            <Box sx={{ mt: 2, p: 2, bgcolor: 'warning.light', borderRadius: 1 }}>
              <Typography variant="subtitle2" color="warning.dark">Instrucciones Especiales:</Typography>
              <Typography variant="body2">{order.specialInstructions}</Typography>
            </Box>
          )}

          {order.isGift && order.giftMessage && (
            <Box sx={{ mt: 2, p: 2, bgcolor: 'secondary.light', borderRadius: 1 }}>
              <Typography variant="subtitle2" color="secondary.dark">Mensaje de Regalo:</Typography>
              <Typography variant="body2" sx={{ fontStyle: 'italic' }}>"{order.giftMessage}"</Typography>
            </Box>
          )}
        </CardContent>
      </Card>

      <Grid container spacing={3}>
        <Grid item xs={12} lg={8}>
          <Card sx={{ mb: 3 }}>
            <CardContent>
              <Box sx={{ display: 'flex', alignItems: 'center', gap: 1, mb: 2 }}>
                <Inventory color="primary" />
                <Typography variant="h6">Productos del Pedido ({items.length})</Typography>
              </Box>
              <TableContainer>
                <Table size="small">
                  <TableHead>
                    <TableRow>
                      <TableCell>Producto</TableCell>
                      <TableCell align="center">Cant.</TableCell>
                      <TableCell align="right">Precio Unit.</TableCell>
                      <TableCell align="right">Dcto.</TableCell>
                      <TableCell align="right">Total</TableCell>
                      <TableCell align="center">Estado</TableCell>
                    </TableRow>
                  </TableHead>
                  <TableBody>
                    {items.map((item) => (
                      <TableRow key={item.id} sx={item.returnReason ? { bgcolor: 'error.light' } : {}}>
                        <TableCell>
                          <Typography variant="body2" fontWeight={500}>{item.productName}</Typography>
                          <Typography variant="caption" color="text.secondary">{item.sku} • {item.category}</Typography>
                          {item.returnReason && (
                            <Typography variant="caption" color="error.main" display="block">
                              Devolución: {item.returnReason}
                            </Typography>
                          )}
                        </TableCell>
                        <TableCell align="center">{item.quantity}</TableCell>
                        <TableCell align="right">{formatCurrency(item.unitPrice)}</TableCell>
                        <TableCell align="right">{item.discountPercent > 0 ? `${item.discountPercent}%` : '-'}</TableCell>
                        <TableCell align="right" sx={{ fontWeight: 500 }}>{formatCurrency(item.lineTotal)}</TableCell>
                        <TableCell align="center">
                          <Chip label={translateStatus(item.status)} size="small" color={getStatusColor(item.status)} />
                        </TableCell>
                      </TableRow>
                    ))}
                  </TableBody>
                </Table>
              </TableContainer>
            </CardContent>
          </Card>

          <Card sx={{ mb: 3 }}>
            <CardContent>
              <Box sx={{ display: 'flex', alignItems: 'center', gap: 1, mb: 2 }}>
                <History color="primary" />
                <Typography variant="h6">Historial de Estados</Typography>
              </Box>
              <Stepper orientation="vertical">
                {statusHistory.map((status, index) => (
                  <Step key={status.id} active={true} completed={index < statusHistory.length - 1}>
                    <StepLabel
                      StepIconComponent={() => (
                        <Box sx={{
                          width: 24,
                          height: 24,
                          borderRadius: '50%',
                          bgcolor: index === statusHistory.length - 1 ? 'primary.main' : 'grey.400',
                          display: 'flex',
                          alignItems: 'center',
                          justifyContent: 'center',
                        }}>
                          {status.status.toLowerCase().includes('delivered') ? (
                            <CheckCircle sx={{ fontSize: 16, color: 'white' }} />
                          ) : status.status.toLowerCase().includes('issue') ? (
                            <Error sx={{ fontSize: 16, color: 'white' }} />
                          ) : (
                            <Schedule sx={{ fontSize: 14, color: 'white' }} />
                          )}
                        </Box>
                      )}
                    >
                      <Box sx={{ display: 'flex', justifyContent: 'space-between', alignItems: 'flex-start' }}>
                        <Box>
                          <Typography variant="subtitle2">{translateStatus(status.status)}</Typography>
                          <Typography variant="caption" color="text.secondary">
                            {status.updatedBy} • {status.location}
                          </Typography>
                        </Box>
                        <Typography variant="caption" color="text.secondary">
                          {formatDate(status.timestamp)}
                        </Typography>
                      </Box>
                    </StepLabel>
                    <StepContent>
                      <Typography variant="body2" color="text.secondary">{status.notes}</Typography>
                    </StepContent>
                  </Step>
                ))}
              </Stepper>
            </CardContent>
          </Card>

          <Card>
            <CardContent>
              <Box sx={{ display: 'flex', alignItems: 'center', gap: 1, mb: 2 }}>
                <Notes color="primary" />
                <Typography variant="h6">Notas de Atención ({serviceNotes.length})</Typography>
              </Box>
              {serviceNotes.map((note) => (
                <Accordion key={note.id} defaultExpanded={note.priority === 'High' || note.priority === 'Urgent'}>
                  <AccordionSummary expandIcon={<ExpandMore />}>
                    <Box sx={{ display: 'flex', alignItems: 'center', gap: 1, width: '100%' }}>
                      <Chip label={translateNoteType(note.noteType)} size="small" variant="outlined" />
                      <Chip label={translatePriority(note.priority)} size="small" color={getPriorityColor(note.priority)} />
                      {note.isInternal && <Chip label="Interno" size="small" sx={{ bgcolor: 'grey.200' }} />}
                      {note.requiresFollowup && <Warning color="warning" sx={{ fontSize: 18 }} />}
                      <Box sx={{ flexGrow: 1 }} />
                      <Typography variant="caption" color="text.secondary">
                        {note.agentName} • {formatDate(note.createdAt)}
                      </Typography>
                    </Box>
                  </AccordionSummary>
                  <AccordionDetails>
                    <Typography variant="body2">{note.content}</Typography>
                    {note.followupDate && (
                      <Typography variant="caption" color="warning.main" sx={{ mt: 1, display: 'block' }}>
                        Seguimiento programado: {formatDate(note.followupDate)}
                      </Typography>
                    )}
                  </AccordionDetails>
                </Accordion>
              ))}
            </CardContent>
          </Card>
        </Grid>

        <Grid item xs={12} lg={4}>
          <Card sx={{ mb: 3 }}>
            <CardContent>
              <Box sx={{ display: 'flex', alignItems: 'center', gap: 1, mb: 2 }}>
                <Person color="primary" />
                <Typography variant="h6">Cliente</Typography>
                {getCustomerTypeIcon(customer.customerType)}
                <Chip label={translateCustomerType(customer.customerType)} size="small" color={customer.customerType === 'VIP' ? 'warning' : 'default'} />
              </Box>
              
              <Typography variant="subtitle1" fontWeight={600}>{customer.fullName}</Typography>
              <Typography variant="caption" color="text.secondary">
                Cliente desde {formatDate(customer.dateJoined)}
              </Typography>

              <Divider sx={{ my: 2 }} />

              <Box sx={{ display: 'flex', flexDirection: 'column', gap: 1.5 }}>
                <Box sx={{ display: 'flex', alignItems: 'center', gap: 1 }}>
                  <Email sx={{ fontSize: 18, color: 'text.secondary' }} />
                  <Link href={`mailto:${customer.email}`} underline="hover">{customer.email}</Link>
                </Box>
                <Box sx={{ display: 'flex', alignItems: 'center', gap: 1 }}>
                  <Phone sx={{ fontSize: 18, color: 'text.secondary' }} />
                  <Link href={`tel:${customer.phone}`} underline="hover">{customer.phone}</Link>
                </Box>
                <Typography variant="caption" color="text.secondary">
                  Contacto preferido: {customer.preferredContactMethod === 'Email' ? 'Correo' : customer.preferredContactMethod === 'Phone' ? 'Teléfono' : customer.preferredContactMethod}
                </Typography>
              </Box>

              {customer.notes && (
                <Box sx={{ mt: 2, p: 1.5, bgcolor: 'grey.50', borderRadius: 1 }}>
                  <Typography variant="caption" color="text.secondary">Notas del Cliente:</Typography>
                  <Typography variant="body2">{customer.notes}</Typography>
                </Box>
              )}

              <Divider sx={{ my: 2 }} />

              <Typography variant="subtitle2" gutterBottom>Direcciones</Typography>
              {customer.addresses.map((addr, idx) => (
                <Box key={idx} sx={{ mb: 1.5, p: 1.5, border: '1px solid', borderColor: 'grey.200', borderRadius: 1 }}>
                  <Box sx={{ display: 'flex', alignItems: 'center', gap: 1, mb: 0.5 }}>
                    <LocationOn sx={{ fontSize: 16, color: 'text.secondary' }} />
                    <Typography variant="caption" fontWeight={500}>
                      {translateAddressType(addr.addressType)} {addr.isDefault && '(Principal)'}
                    </Typography>
                  </Box>
                  <Typography variant="body2">{addr.street}</Typography>
                  <Typography variant="body2">{addr.city}, {addr.state} {addr.postalCode}</Typography>
                  <Typography variant="body2">{addr.country}</Typography>
                </Box>
              ))}
            </CardContent>
          </Card>

          {shipping && (
            <Card sx={{ mb: 3 }}>
              <CardContent>
                <Box sx={{ display: 'flex', alignItems: 'center', gap: 1, mb: 2 }}>
                  <LocalShipping color="primary" />
                  <Typography variant="h6">Envío</Typography>
                </Box>

                <Grid container spacing={1}>
                  <Grid item xs={6}>
                    <Typography variant="caption" color="text.secondary">Transportista</Typography>
                    <Typography variant="body2">{shipping.carrier}</Typography>
                  </Grid>
                  <Grid item xs={6}>
                    <Typography variant="caption" color="text.secondary">Servicio</Typography>
                    <Typography variant="body2">{shipping.serviceType}</Typography>
                  </Grid>
                  <Grid item xs={12}>
                    <Typography variant="caption" color="text.secondary">N° Seguimiento</Typography>
                    {shipping.trackingUrl ? (
                      <Link href={shipping.trackingUrl} target="_blank" underline="hover" display="block">
                        {shipping.trackingNumber}
                      </Link>
                    ) : (
                      <Typography variant="body2">{shipping.trackingNumber}</Typography>
                    )}
                  </Grid>
                  <Grid item xs={6}>
                    <Typography variant="caption" color="text.secondary">Peso</Typography>
                    <Typography variant="body2">{shipping.shippingWeight} kg</Typography>
                  </Grid>
                  <Grid item xs={6}>
                    <Typography variant="caption" color="text.secondary">Bultos</Typography>
                    <Typography variant="body2">{shipping.packageCount}</Typography>
                  </Grid>
                  {shipping.shippedDate && (
                    <Grid item xs={6}>
                      <Typography variant="caption" color="text.secondary">Despachado</Typography>
                      <Typography variant="body2">{formatDate(shipping.shippedDate)}</Typography>
                    </Grid>
                  )}
                  {shipping.deliveredDate && (
                    <Grid item xs={6}>
                      <Typography variant="caption" color="text.secondary">Entregado</Typography>
                      <Typography variant="body2">{formatDate(shipping.deliveredDate)}</Typography>
                    </Grid>
                  )}
                  {shipping.deliverySignature && (
                    <Grid item xs={12}>
                      <Typography variant="caption" color="text.secondary">Recibido por</Typography>
                      <Typography variant="body2">{shipping.deliverySignature}</Typography>
                    </Grid>
                  )}
                </Grid>

                <Divider sx={{ my: 2 }} />

                <Typography variant="subtitle2" gutterBottom>Dirección de Entrega</Typography>
                <Typography variant="body2">{shipping.shippingAddress.street}</Typography>
                <Typography variant="body2">
                  {shipping.shippingAddress.city}, {shipping.shippingAddress.state} {shipping.shippingAddress.postalCode}
                </Typography>
                <Typography variant="body2">{shipping.shippingAddress.country}</Typography>

                {shipping.deliveryInstructions && (
                  <Box sx={{ mt: 2, p: 1.5, bgcolor: 'info.light', borderRadius: 1 }}>
                    <Typography variant="caption" color="info.dark">Instrucciones de Entrega:</Typography>
                    <Typography variant="body2">{shipping.deliveryInstructions}</Typography>
                  </Box>
                )}
              </CardContent>
            </Card>
          )}

          {payment && (
            <Card>
              <CardContent>
                <Box sx={{ display: 'flex', alignItems: 'center', gap: 1, mb: 2 }}>
                  <Payment color="primary" />
                  <Typography variant="h6">Pago</Typography>
                </Box>

                <Grid container spacing={1}>
                  <Grid item xs={6}>
                    <Typography variant="caption" color="text.secondary">Método</Typography>
                    <Typography variant="body2">{payment.paymentMethod === 'CreditCard' ? 'Tarjeta de Crédito' : payment.paymentMethod === 'BankTransfer' ? 'Transferencia' : payment.paymentMethod}</Typography>
                  </Grid>
                  <Grid item xs={6}>
                    <Typography variant="caption" color="text.secondary">Estado</Typography>
                    <Chip label={translateStatus(payment.paymentStatus)} size="small" color={getStatusColor(payment.paymentStatus)} />
                  </Grid>
                  {payment.cardBrand && (
                    <Grid item xs={12}>
                      <Typography variant="caption" color="text.secondary">Tarjeta</Typography>
                      <Typography variant="body2">{payment.cardBrand} ****{payment.cardLastFour}</Typography>
                    </Grid>
                  )}
                  <Grid item xs={12}>
                    <Typography variant="caption" color="text.secondary">ID Transacción</Typography>
                    <Typography variant="body2" sx={{ fontFamily: 'monospace', fontSize: '0.85rem' }}>
                      {payment.transactionId}
                    </Typography>
                  </Grid>
                  <Grid item xs={6}>
                    <Typography variant="caption" color="text.secondary">Monto Pagado</Typography>
                    <Typography variant="body2" fontWeight={500}>{formatCurrency(payment.amountPaid)}</Typography>
                  </Grid>
                  <Grid item xs={6}>
                    <Typography variant="caption" color="text.secondary">Fecha de Pago</Typography>
                    <Typography variant="body2">{formatDate(payment.paymentDate)}</Typography>
                  </Grid>
                  {payment.refundedAmount && payment.refundedAmount > 0 && (
                    <Grid item xs={12}>
                      <Typography variant="caption" color="error.main">Reembolsado</Typography>
                      <Typography variant="body2" color="error.main">{formatCurrency(payment.refundedAmount)}</Typography>
                    </Grid>
                  )}
                </Grid>

                <Divider sx={{ my: 2 }} />

                <Typography variant="subtitle2" gutterBottom>Dirección de Facturación</Typography>
                <Typography variant="body2">{payment.billingAddress.street}</Typography>
                <Typography variant="body2">
                  {payment.billingAddress.city}, {payment.billingAddress.state} {payment.billingAddress.postalCode}
                </Typography>
                <Typography variant="body2">{payment.billingAddress.country}</Typography>
              </CardContent>
            </Card>
          )}
        </Grid>
      </Grid>
    </Box>
  );
}