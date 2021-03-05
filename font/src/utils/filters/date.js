import { formatDate } from '@/utils/date'

export function timeFilter(value, fmt = "yyyy-MM-dd hh:mm:ss") {
  if (!value) return ''
  return formatDate(value.toString(), fmt)
}