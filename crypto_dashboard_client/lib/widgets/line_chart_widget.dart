import 'package:fl_chart/fl_chart.dart';
import 'package:flutter/material.dart';
import '../models/crypto_pair.dart';

class LineChartWidget extends StatelessWidget {
  final List<CryptoPair> cryptoPairs;

  LineChartWidget({required this.cryptoPairs});

  List<FlSpot> getSpots() {
    return cryptoPairs
        .asMap()
        .entries
        .map((e) => FlSpot(e.key.toDouble(), e.value.price))
        .toList();
  }

  @override
  Widget build(BuildContext context) {
    return LineChart(
      LineChartData(
        lineBarsData: [
          LineChartBarData(
            spots: getSpots(),
            isCurved: true,
            barWidth: 2,
            colors: [Colors.blue],
            belowBarData: BarAreaData(show: false),
          ),
        ],
        titlesData: FlTitlesData(
          leftTitles: SideTitles(showTitles: true),
          bottomTitles: SideTitles(showTitles: true),
        ),
        borderData: FlBorderData(
          show: true,
          border: Border.all(color: Colors.grey),
        ),
      ),
    );
  }
}
