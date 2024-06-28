class CryptoPair {
  final String pairName;
  final DateTime date;
  final double open;
  final double close;
  final double high;
  final double low;
  final double price;
  final int volume;
  final double changePercentage;

  CryptoPair({
    required this.pairName,
    required this.date,
    required this.open,
    required this.close,
    required this.high,
    required this.low,
    required this.price,
    required this.volume,
    required this.changePercentage,
  });

  factory CryptoPair.fromJson(Map<String, dynamic> json) {
    return CryptoPair(
      pairName: json['pairName'],
      date: DateTime.parse(json['date']),
      open: json['open'].toDouble(),
      close: json['close'].toDouble(),
      high: json['high'].toDouble(),
      low: json['low'].toDouble(),
      price: json['price'].toDouble(),
      volume: json['volume'],
      changePercentage: json['changePercentage'].toDouble(),
    );
  }
}
